using HiFive.Application.Contracts.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class DistributorController : ControllerBase
{
	private readonly IDistributorService _distributorService;
	private readonly IArtistService _artistService;
	private readonly ICurrentUserService _currentUserService;

	public DistributorController(IDistributorService distributorService, ICurrentUserService currentUserService, IArtistService artistService)
	{
		_distributorService = distributorService;
		_currentUserService = currentUserService;
		_artistService = artistService;
	}

	[HttpPost("add-artist/{id}")]
	[Authorize(Roles = "Distributor")]
	public async Task<IActionResult> AddArtist(Guid id)
	{
		await _distributorService.AddArtistToDistributor(_currentUserService.Id, id);
		return NoContent();
	}

	[HttpPost("remove-artist/{id}")]
	[Authorize(Roles = "Distributor")]
	public async Task<IActionResult> RemoveArtist(Guid id)
	{
		await _distributorService.RemoveArtistFromDistributor(_currentUserService.Id, id);
		return NoContent();
	}

	[HttpGet("get-artists")]
	[Authorize(Roles = "Distributor")]
	public async Task<IActionResult> GetArtists()
	{
		var artists = await _artistService.GetArtistsByDistributorId(_currentUserService.Id);
		return Ok(artists);
	}

}
