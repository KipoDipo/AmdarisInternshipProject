using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Song;
using HiFive.Presentation.Controllers.Requests.Music;
using HiFive.Presentation.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class SongController : ControllerBase
{
	private ISongService _songService;
	private IImageFileService _imageFileService;

	public SongController(ISongService songService, IImageFileService imageFileService)
	{
		_songService = songService;
		_imageFileService = imageFileService;
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromForm] SongCreateRequest song)
	{
		var imageCreateDto = ImageDtoHelper.CreateDtoFromFormFile(song.CoverImage);
		var imageDto = await _imageFileService.UploadImageAsync(imageCreateDto);
		var songDto = song.ToSongCreateDto(imageDto.Id);
		return Ok(await _songService.CreateSongAsync(songDto));
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

	[HttpGet("artist/{artistId}")]
	public async Task<IActionResult> GetByArtistId(Guid artistId)
	{
		return Ok(await _songService.GetSongsByArtistIdAsync(artistId));
	}
}
