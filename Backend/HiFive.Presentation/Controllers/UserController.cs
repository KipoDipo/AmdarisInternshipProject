using HiFive.Application.AwardSystem.BadgeStrategies;
using HiFive.Presentation.Controllers.Requests.Listener;
using HiFive.Presentation.Extentions;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HiFive.Infrastructure.Identity;
using HiFive.Application.Services;
using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.AwardSystem;
using HiFive.Presentation.Controllers.Requests.Users;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly JwtService _jwtService;
	private readonly IListenerService _listenerService;
	private readonly IArtistService _artistService;
	//private readonly _distributorService;
	//private readonly _adminService;
	private readonly IImageFileService _imageFileService;
	private readonly Awarder _awarder;

	public UserController(JwtService jwtService, UserManager<ApplicationUser> userManager, IListenerService listenerService, IImageFileService imageFileService, Awarder awarder, IArtistService artistService)
	{
		_jwtService = jwtService;
		_userManager = userManager;
		_listenerService = listenerService;
		_imageFileService = imageFileService;
		_awarder = awarder;
		_artistService = artistService;
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
	{
		var user = await _userManager.FindByEmailAsync(loginRequest.Email);
		if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
			return Unauthorized(new { Message = "Invalid Credentials" });

		var token = await _jwtService.GenerateToken(user);
		return Ok(new { token });
	}

	[HttpPost("listener")]
	public async Task<IActionResult> Create([FromForm] ListenerCreateRequest listener)
	{
		Guid? imageId = null;

		if (listener.ProfilePicture != null)
		{
			var imageDto = await _imageFileService.UploadImageAsync(ImageDtoHelper.CreateDtoFromFormFile(listener.ProfilePicture));
			imageId = imageDto.Id;
		}
		var dto = await _listenerService.CreateListenerAsync(listener.ToListenerCreateDto(imageId));
		await _awarder.Award(dto.Id, new Registered());
		return Ok(dto);
	}

	[HttpPost("artist")]
	public async Task<IActionResult> Create(ArtistCreateRequest artist)
	{
		Guid? imageId = null;

		if (artist.ProfilePicture != null)
		{
			var imageDto = await _imageFileService.UploadImageAsync(ImageDtoHelper.CreateDtoFromFormFile(artist.ProfilePicture));
			imageId = imageDto.Id;
		}

		var artistDto = artist.ToArtistCreateDto(imageId);
		return Ok(await _artistService.CreateArtistAsync(artistDto));
	}
}
