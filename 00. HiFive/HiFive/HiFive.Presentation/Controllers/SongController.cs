using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Song;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class SongController : ControllerBase
{
	private ISongService _songService;

	public SongController(ISongService songService)
	{
		_songService = songService;
	}

	[HttpPost]
	public async Task<IActionResult> Create(SongCreateDto song)
	{
		return Ok(await _songService.CreateSongAsync(song));
	}

	[HttpGet("id/{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		return Ok(await _songService.GetSongByIdAsync(id));
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
