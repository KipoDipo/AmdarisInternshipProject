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

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		return Ok(await _songService.GetSongByIdAsync(id));
	}
}
