using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Genre;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController : ControllerBase
{
	private readonly IGenreService _genreService;

	public GenreController(IGenreService genreService)
	{
		_genreService = genreService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateGenre(GenreCreateDto genre)
	{
		return Ok(await _genreService.CreateGenreAsync(genre));
	}

	[HttpGet]
	public async Task<IActionResult> GetAllGenres()
	{
		return Ok(await _genreService.GetAllGenresAsync());
	}

	[HttpGet("id/{id}")]
	public async Task<IActionResult> GetGenreById(Guid id)
	{
		return Ok(await _genreService.GetGenreByIdAsync(id));
	}

	[HttpGet("song/{songId}")]
	public async Task<IActionResult> GetAllGenresBySongId(Guid songId)
	{
		return Ok(await _genreService.GetAllGenresBySongIdAsync(songId));
	}

	[HttpGet("partial/{partialName}")]
	public async Task<IActionResult> GetAllGenresByPartialName(string partialName)
	{
		return Ok(await _genreService.GetAllGenresByPartialNameAsync(partialName));
	}

	[HttpPut("id/{id}")]
	public async Task<IActionResult> UpdateGenre(GenreDto genre)
	{
		await _genreService.UpdateGenreAsync(genre);
		return NoContent();
	}

	[HttpDelete("id/{id}")]
	public async Task<IActionResult> DeleteGenre(Guid id)
	{
		await _genreService.DeleteGenreAsync(id);
		return NoContent();
	}
}
