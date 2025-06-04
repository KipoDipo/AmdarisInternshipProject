using HiFive.Application.AwardSystem;
using HiFive.Application.AwardSystem.BadgeStrategies;
using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Listener;
using HiFive.Presentation.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Authorize(Roles = "Listener")]
[Route("[controller]")]
public class ListenerController : ControllerBase
{
	private readonly IListenerService _listenerService;
	private readonly IImageFileService _imageFileService;
	private readonly ICurrentUserService _currentUserService;
	private readonly Awarder _awarder;

	public ListenerController(IListenerService listenerService, IImageFileService imageFileService, ICurrentUserService currentUserService, Awarder awarder)
	{
		_listenerService = listenerService;
		_imageFileService = imageFileService;
		_currentUserService = currentUserService;
		_awarder = awarder;
	}

	[HttpPost("like/{songId}")]
	public async Task<IActionResult> LikeSong(Guid songId)
	{
		await _listenerService.LikeSongAsync(_currentUserService.Id, songId);
		return NoContent();
	}

	[HttpPost("unlike/{songId}")]
	public async Task<IActionResult> UnlikeSong(Guid songId)
	{
		await _listenerService.UnlikeSongAsync(_currentUserService.Id, songId);
		return NoContent();
	}

	[HttpPost("follow-artist/{artistId}")]
	public async Task<IActionResult> FollowArtist(Guid artistId)
	{
		await _listenerService.FollowArtistAsync(_currentUserService.Id, artistId);
		var following = await _listenerService.GetFollowingArtists(_currentUserService.Id);
		if (following.Count() == 5)
			await _awarder.Award(_currentUserService.Id, new Followed5Artists());
		return NoContent();
	}

	[HttpPost("unfollow-artist/{artistId}")]
	public async Task<IActionResult> UnfollowArtist(Guid artistId)
	{
		await _listenerService.UnfollowArtistAsync(_currentUserService.Id, artistId);
		return NoContent();
	}

	[HttpGet("following-artists")]
	public async Task<IActionResult> GetFollowingdArtists()
	{
		return Ok(await _listenerService.GetFollowingArtists(_currentUserService.Id));
	}


	[HttpGet("id/{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		return Ok(await _listenerService.GetListenerByIdAsync(id));
	}

	[HttpGet]
	public async Task<IActionResult> GetMe()
	{
		return await GetById(_currentUserService.Id);
	}

	[HttpGet("details")]
	public async Task<IActionResult> GetMeWithDetail()
	{
		return await GetDetailsById(_currentUserService.Id);
	}

	[HttpGet("name/{partialName}")]
	public async Task<IActionResult> GetByPartialName(string partialName)
	{
		return Ok(await _listenerService.GetListenersByPartialNameAsync(partialName));
	}

	[HttpGet("details/{id}")]
	public async Task<IActionResult> GetDetailsById(Guid id)
	{
		return Ok(await _listenerService.GetListenerDetailsByIdAsync(id));
	}

	[HttpPut("update")]
	public async Task<IActionResult> UpdateMe([FromForm] ListenerUpdateMeRequest request)
	{
		var id = _currentUserService.Id;
		if (request.ProfilePicture is not null)
		{
			var imageDto = await _imageFileService.UploadImageAsync(ImageDtoHelper.CreateDtoFromFormFile(request.ProfilePicture));
			var listenerUpdateDto = request.ToListenerUpdateDto(id, imageDto.Id);
			await _awarder.Award(id, new UploadedProfilePicture());
			await Update(listenerUpdateDto);
			return NoContent();
		}
		else
		{
			var listenerUpdateDto = request.ToListenerUpdateDto(id, null);
			await Update(listenerUpdateDto);
			return NoContent();
		}
	}

	[HttpPut]
	public async Task<IActionResult> Update([FromForm] ListenerUpdateDto listener)
	{
		await _listenerService.UpdateListenerAsync(listener);
		return NoContent();
	}
}
