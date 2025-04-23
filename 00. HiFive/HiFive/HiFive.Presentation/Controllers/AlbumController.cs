using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Album;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumController : ControllerBase
{
	private readonly IAlbumService _albumService;
	
	public AlbumController(IAlbumService albumService)
	{
		_albumService = albumService;
	}
	
	[HttpPost]
	public async Task<IActionResult> Create(AlbumCreateDto album)
	{
		return Ok(await _albumService.CreateAlbumAsync(album));
	}
	
	[HttpGet("id/{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		return Ok(await _albumService.GetAlbumByIdAsync(id));
	}

	[HttpGet("details/{id}")]
	public async Task<IActionResult> GetDetailsById(Guid id)
	{
		return Ok(await _albumService.GetAlbumDetailsByIdAsync(id));
	}

	[HttpGet("artist/{id}")]
	public async Task<IActionResult> GetByArtistId(Guid id)
	{
		return Ok(await _albumService.GetAllAlbumsByArtistAsync(id));
	}

	[HttpGet("name/{partialName}")]
	public async Task<IActionResult> GetByPartialName(string partialName)
	{
		return Ok(await _albumService.GetAllAlbumsByPartialTitleAsync(partialName));
	}

	[HttpPut]
	public async Task<IActionResult> Update(AlbumUpdateDto album)
	{
		await _albumService.UpdateAlbumAsync(album);
		return NoContent();
	}
}
