using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Presentation.Controllers.Requests.Users;
using HiFive.Presentation.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ArtistController : ControllerBase
{
	private readonly IArtistService _artistService;
	private readonly IImageFileService _imageFileService;

	public ArtistController(IArtistService artistService, IImageFileService imageFileService)
	{
		_artistService = artistService;
		_imageFileService = imageFileService;
	}

	[HttpPost]
	public async Task<IActionResult> Create(ArtistCreateRequest artist)
	{
		if (artist.ProfilePicture == null)
		{
			var artistDto = artist.ToArtistCreateDto(null);

			return Ok(await _artistService.CreateArtistAsync(artistDto));
		}
		else
		{
			var imageDto = await _imageFileService.UploadImageAsync(ImageDtoHelper.CreateDtoFromFormFile(artist.ProfilePicture));

			var artistDto = artist.ToArtistCreateDto(imageDto.Id);

			return Ok(await _artistService.CreateArtistAsync(artistDto));
		}
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
	public async Task<IActionResult> Update(ArtistUpdateRequest artistRequest)
	{
		var artistUpdateDto = artistRequest.ToAristUpdateDto();

		if (artistRequest.ProfilePicture == null)
		{
			await _artistService.UpdateArtistAsync(artistUpdateDto);
			return NoContent();
		}

		var artistDto = await _artistService.GetArtistByIdAsync(artistRequest.Id);
		if (artistDto.ProfilePictureId == null) // if artist doesn't have a profile picture already
		{
			var imageDto = await _imageFileService.UploadImageAsync(ImageDtoHelper.CreateDtoFromFormFile(artistRequest.ProfilePicture));

			artistUpdateDto.ProfilePictureId = imageDto.Id;
		}
		else // if artist already has a profile picture
		{
			var imageDto = await _imageFileService.GetImageByIdAsync(artistDto.ProfilePictureId.Value);

			var imageUpdateDto = ImageDtoHelper.UpdateDtoFromFormFile(artistRequest.ProfilePicture, imageDto.Id);
			await _imageFileService.UpdateImageAsync(imageUpdateDto);
		}

		await _artistService.UpdateArtistAsync(artistUpdateDto);
		return NoContent();
	}
}
