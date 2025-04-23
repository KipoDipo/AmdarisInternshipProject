using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Artist;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ArtistController : ControllerBase
{
	private readonly IArtistService _artistService;

	public ArtistController(IArtistService artistService)
	{
		_artistService = artistService;
	}

	[HttpPost]
	public async Task<IActionResult> Create(ArtistCreateDto artist)
	{
		return Ok(await _artistService.CreateArtistAsync(artist));
	}

	[HttpGet("id/{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		return Ok(await _artistService.GetArtistByIdAsync(id));
	}

	[HttpGet("name/{partialName}")]
	public async Task<IActionResult> GetByPartialName(string partialName)
	{
		return Ok(await _artistService.GetArtistsByPartialNameAsync(partialName));
	}

	[HttpGet("details/{id}")]
	public async Task<IActionResult> GetDetailsById(Guid id)
	{
		return Ok(await _artistService.GetArtistDetailsByIdAsync(id));
	}

	[HttpPut]
	public async Task<IActionResult> Update(ArtistUpdateDto artist)
	{
		await _artistService.UpdateArtistAsync(artist);
		return NoContent();
	}
}
