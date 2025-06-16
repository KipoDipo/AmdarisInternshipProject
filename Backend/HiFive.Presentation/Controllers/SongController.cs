using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Artist;
using HiFive.Application.DTOs.Genre;
using HiFive.Application.DTOs.Misc;
using HiFive.Application.DTOs.Song;
using HiFive.Domain.Models.Music;
using HiFive.Presentation.Controllers.Requests.Music;
using HiFive.Presentation.Controllers.Requests.Song;
using HiFive.Presentation.Extentions;
using HiFive.Presentation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class SongController : ControllerBase
{
	private ISongService _songService;
	private IArtistService _artistService;
	private IImageFileService _imageFileService;
	private ICurrentUserService _currentUserService;
	private IListenerDataService _listenerDataService;
	private IGenreService _genreService;
	private BlobService _blobService;

	public SongController(ISongService songService, IImageFileService imageFileService, ICurrentUserService currentUserService, BlobService blobService, IListenerDataService listenerDataService, IArtistService artistService, IGenreService genreService)
	{
		_songService = songService;
		_imageFileService = imageFileService;
		_currentUserService = currentUserService;
		_blobService = blobService;
		_listenerDataService = listenerDataService;
		_artistService = artistService;
		_genreService = genreService;
	}

	[HttpPost]
	[Authorize(Policy = "VerifiedDistributorOnly")]
	public async Task<IActionResult> Create([FromForm] SongCreateRequest song)
	{
		var imageCreateDto = ImageDtoHelper.CreateDtoFromFormFile(song.CoverImage);
		var imageDto = await _imageFileService.UploadImageAsync(imageCreateDto);
		var (duration, songUri) = await _blobService.UploadFileAsync(song.Data);
		var songDto = song.ToSongCreateDto(imageDto.Id, songUri);
		songDto.Duration = duration;
		return Ok(await _songService.CreateSongAsync(songDto));
	}

	[HttpPut]
	[Authorize(Policy = "VerifiedDistributorOnly")]
	public async Task<IActionResult> Update([FromForm] SongUpdateRequest song)
	{
		var songDto = await _songService.GetSongByIdAsync(song.Id);

		ImageCreateDto? imageCreateDto = null;
		SongUpdateDto updateDto = new SongUpdateDto();

		updateDto.Id = songDto.Id;

		if (song.CoverImage != null)
		{
			imageCreateDto = ImageDtoHelper.CreateDtoFromFormFile(song.CoverImage);
			if (songDto.CoverImageId == null)
			{
				var imageDto = await _imageFileService.UploadImageAsync(imageCreateDto);
				updateDto.CoverImageId = imageDto.Id;
			}
			else
			{
				ImageUpdateDto imageUpdateDto = new ImageUpdateDto()
				{
					Id = (Guid)songDto.CoverImageId,
					Data = imageCreateDto.Data,
					ContentType = imageCreateDto.ContentType
				};
				await _imageFileService.UpdateImageAsync(imageUpdateDto);
			}
		}

		if (song.Data != null)
		{
			var file = await _blobService.UploadFileAsync(song.Data);
			// TODO: Delete old file
			updateDto.Data = file.Uri;
			updateDto.Duration = file.Duration;
		}

		updateDto.AlbumId = song.AlbumId;
		updateDto.Title = song.Title;

		await _songService.UpdateSongAsync(updateDto);
		return NoContent();
	}

	[HttpGet("download/{songId}")]
	public async Task<IActionResult> DownloadSong(Guid songId)
	{
		var song = await _songService.GetSongByIdAsync(songId);

		await _listenerDataService.AddListenedSong(_currentUserService.Id, songId);

		var sasUrl = _blobService.GetSasUrl(song.Data, TimeSpan.FromHours(0.5));

		return Ok(new { url = sasUrl });
	}

	[HttpGet("id/{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		return Ok(await _songService.GetSongByIdAsync(id));
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		return Ok(await _songService.GetAllSongsAsync());
	}

	[HttpGet("details/{id}")]
	public async Task<IActionResult> GetDetailsById(Guid id)
	{
		return Ok(await _songService.GetSongDetailsByIdAsync(id));
	}

	[HttpGet("name/{partialName}")]
	public async Task<IActionResult> GetByPartialName(string partialName)
	{
		return Ok(await _songService.GetSongsByPartialNameAsync(partialName));
	}

	[HttpGet("genre/{genreId}")]
	public async Task<IActionResult> GetByGenre(Guid genreId, [FromQuery] PagingParameters pagingParameters)
	{
		return Ok(await _songService.GetSongsByGenreAsync(genreId, pagingParameters.PageNumber, pagingParameters.PageSize));
	}

	[HttpGet("playlist/{id}")]
	public async Task<IActionResult> GetSongsByPlaylistId(Guid id)
	{
		return Ok(await _songService.GetAllSongsByPlaylistIdAsync(id));
	}

	[HttpGet("album/{id}")]
	public async Task<IActionResult> GetSongsByAlbumId(Guid id)
	{
		return Ok(await _songService.GetAllSongsByAlbumIdAsync(id));
	}

	[HttpGet("my-history")]
	public async Task<IActionResult> GetSongsFromHistory([FromQuery] PagingParameters pagingParameters, int lastNMonths = 1)
	{
		var songs = await _listenerDataService.GetUniqueSongsListenedById(_currentUserService.Id, pagingParameters.PageNumber, pagingParameters.PageSize, lastNMonths);
		return Ok(songs);
	}
	
	[HttpGet("history/{id}")]
	public async Task<IActionResult> GetSongsFromHistory(Guid id, [FromQuery] PagingParameters pagingParameters, int lastNMonths = 1)
	{
		var songs = await _listenerDataService.GetUniqueSongsListenedById(id, pagingParameters.PageNumber, pagingParameters.PageSize, lastNMonths);
		return Ok(songs);
	}

	[HttpGet("curated-songs")]
	public async Task<IActionResult> GetCuratedSongs()
	{
		var lynyrd = (await _artistService.GetArtistsByPartialNameAsync("Lynyrd Skynyrd")).First();
		var reol = (await _artistService.GetArtistsByPartialNameAsync("Reol")).First();
		var lotus = (await _artistService.GetArtistsByPartialNameAsync("Lotus Juice")).First();

		var songs1 = (await _songService.GetSongsByArtistIdAsync(lynyrd.Id, 1, 5));
		var songs2 = (await _songService.GetSongsByArtistIdAsync(reol.Id, 1, 5));
		var songs3 = (await _songService.GetSongsByArtistIdAsync(lotus.Id, 1, 5));

		List<SongDto> songs = [];
		songs.AddRange(songs1);
		songs.AddRange(songs2);
		songs.AddRange(songs3);

		return Ok(songs);
	}

	[HttpGet("liked/{listenerId}")]
	public async Task<IActionResult> GetListenerLikedSongs(Guid listenerId)
	{
		return Ok(await _songService.GetListenerLikedSongs(listenerId));
	}

	[HttpGet("my-liked")]
	public async Task<IActionResult> GetMyLikedSongs()
	{
		return Ok(await _songService.GetListenerLikedSongs(_currentUserService.Id));
	}

	[HttpGet("my-recommended")]
	public async Task<IActionResult> GetRecommendedForMe([FromQuery] PagingParameters pagingParameters, int countPerGenre)
	{
		var songs = await _listenerDataService.GetUniqueSongsListenedById(_currentUserService.Id, pagingParameters.PageNumber, pagingParameters.PageSize, 2);
		var genreIds = songs.SelectMany(s => s.GenreIds).Distinct();
		
		var genres = new List<GenreDto>();
		foreach (var id in genreIds)
			genres.Add(await _genreService.GetGenreByIdAsync(id));

		var result = new List<object>();

		Random rng = new Random();

		foreach (var g in genres)
		{
			var songsByGenre = await _songService.GetRandomSongsByGenre(g.Id, countPerGenre);
			result.Add(new
			{
				Name = g.Name,
				Songs = songsByGenre
			});
		}

		return Ok(result);
	}

	[HttpGet("artist/{artistId}")]
	public async Task<IActionResult> GetByArtistId(Guid artistId, [FromQuery] PagingParameters pagingParameters)
	{
		return Ok(await _songService.GetSongsByArtistIdAsync(artistId, pagingParameters.PageNumber, pagingParameters.PageSize));
	}

	[HttpDelete("{id}")]
	[Authorize(Policy = "VerifiedDistributorOnly")]
	public async Task<IActionResult> Remove(Guid id)
	{
		await _songService.DeleteSongById(id);
		return NoContent();
	}
}
