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
	private readonly ICurrentUserService _currentUserService;

	public DistributorController(IDistributorService distributorService, ICurrentUserService currentUserService)
	{
		_distributorService = distributorService;
		_currentUserService = currentUserService;
	}

	[HttpPost("add-artist/{id}")]
	[Authorize(Roles = "Distributor,Admin")]
	public async Task<IActionResult> AddArtist(Guid id)
	{
		await _distributorService.AddArtistToDistributor(_currentUserService.Id, id);
		return NoContent();
	}
}
