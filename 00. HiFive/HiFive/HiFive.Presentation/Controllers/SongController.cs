using HiFive.Application.DTOs.Song;
using HiFive.Application.Services.Contracts;
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
		return Ok(await _songService.CreateSongAsync(song.Title, song.ArtistId, song.AlbumId, song.Duration, song.GenreIds, song.ReleaseDate, song.Data));
	}

	[HttpGet("id/{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		return Ok(await _songService.GetSongByIdAsync(id));
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

	[HttpGet("details/{id}")]
	public async Task<IActionResult> GetDetailsById(Guid id)
	{
		return Ok(await _songService.GetSongDetailsByIdAsync(id));
	}

	[HttpPut]
	public async Task<IActionResult> Update(SongUpdateDto song)
	{
		return Ok(await _songService.UpdateSongAsync(song.Id, song.Title, song.ReleaseDate));
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
