using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Trophy;
using HiFive.Presentation.Controllers.Requests.Trophies;
using HiFive.Presentation.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class TrophyController : ControllerBase
{
	private ITrophyService _trophyService;
	private IImageFileService _imageFileService;
	private ICurrentUserService _currentUserService;

	public TrophyController(ITrophyService trophyService, IImageFileService imageFileService, ICurrentUserService currentUserService)
	{
		_trophyService = trophyService;
		_imageFileService = imageFileService;
		_currentUserService = currentUserService;
	}

	[HttpGet("get-badge/{id}")]
	public async Task<IActionResult> GetBadgeById(Guid id)
	{
		return Ok(await _trophyService.GetBadgeById(id));
	}

	[HttpGet("get-title/{id}")]
	public async Task<IActionResult> GetTitleById(Guid id)
	{
		return Ok(await _trophyService.GetTitleById(id));
	}

	[HttpGet("get-badge/alt")]
	public async Task<IActionResult> GetBadgeByKey(string key, Guid? artist = null)
	{
		return Ok(await _trophyService.GetBadgeByConditionKeyAndArtist(key, artist));
	}

	[HttpGet("get-title/alt")]
	public async Task<IActionResult> GetTitleByKey(string key, Guid? artist = null)
	{
		return Ok(await _trophyService.GetTitleByConditionKeyAndArtist(key, artist));
	}

	[HttpGet("get-condition")]
	public async Task<IActionResult> GetConditionByKey(string key)
	{
		return Ok(await _trophyService.GetConditionByKey(key));
	}

	[HttpGet("my-badges")]
	public async Task<IActionResult> GetMyBadges()
	{
		return Ok(await _trophyService.GetListenerBadges(_currentUserService.Id));
	}

	[HttpPost("create-condition")]
	public async Task<IActionResult> CreateCondition(ConditionCreateDto dto)
	{
		await _trophyService.CreateCondition(dto);
		return NoContent();
	}

	[HttpPost("create-badge")]
	public async Task<IActionResult> CreateNewBadge(CreateBadgeRequest request)
	{
		var imageDto = await _imageFileService.UploadImageAsync(ImageDtoHelper.CreateDtoFromFormFile(request.Image));
		var dto = request.ToBadgeCreateDto(imageDto.Id);
		await _trophyService.CreateBadge(dto);
		return NoContent();
	}

	[HttpPost("create-title")]
	public async Task<IActionResult> CreateNewTitle(TitleCreateDto dto)
	{
		await _trophyService.CreateTitle(dto);
		return NoContent();
	}

}
