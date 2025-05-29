using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Song;
using HiFive.Presentation.Controllers.Requests.Music;
using HiFive.Presentation.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class SongController : ControllerBase
{
	private ISongService _songService;
	private IImageFileService _imageFileService;
	private ICurrentUserService _currentUserService;
	private IListenerDataService _listenerDataService;
	private BlobService _blobService;

	public SongController(ISongService songService, IImageFileService imageFileService, ICurrentUserService currentUserService, BlobService blobService, IListenerDataService listenerDataService)
	{
		_songService = songService;
		_imageFileService = imageFileService;
		_currentUserService = currentUserService;
		_blobService = blobService;
		_listenerDataService = listenerDataService;
	}

	[HttpPost]
	[Authorize(Roles = "Distributor,Admin")]
	public async Task<IActionResult> Create([FromForm] SongCreateRequest song)
	{
		var imageCreateDto = ImageDtoHelper.CreateDtoFromFormFile(song.CoverImage);
		var imageDto = await _imageFileService.UploadImageAsync(imageCreateDto);
		var (duration, songUri) = await _blobService.UploadFileAsync(song.Data);
		var songDto = song.ToSongCreateDto(imageDto.Id, songUri);
		songDto.Duration = duration;
		return Ok(await _songService.CreateSongAsync(songDto));
	}

	[HttpGet("download/{songId}")]
	public async Task<IActionResult> DownloadSong(Guid songId)
	{
		var song = await _songService.GetSongByIdAsync(songId);

		var (stream, contentType) = await _blobService.DownloadFileAsync(song.Data);

		if (stream == null)
			return NotFound();
		await _listenerDataService.AddListenedSong(_currentUserService.Id, songId);
		return File(stream, contentType);
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
	public async Task<IActionResult> GetByGenre(Guid genreId)
	{
		return Ok(await _songService.GetAllSongsByGenreAsync(genreId));
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

	[HttpGet("my-history/")]
	public async Task<IActionResult> GetSongsFromHistory(int count = 8, int lastNMonths = 1)
	{
		var songs = await _listenerDataService.GetUniqueSongsListenedById(_currentUserService.Id, count, lastNMonths);
		return Ok(songs);
	}

	[HttpPut]
	public async Task<IActionResult> Update(SongUpdateDto song)
	{
		await _songService.UpdateSongAsync(song);
		return NoContent();
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

	[HttpGet("artist/{artistId}")]
	public async Task<IActionResult> GetByArtistId(Guid artistId)
	{
		return Ok(await _songService.GetSongsByArtistIdAsync(artistId));
	}
}
