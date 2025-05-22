using HiFive.Application.Contracts.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
	private readonly IImageFileService _imageService;

	public ImageController(IImageFileService imageService)
	{
		_imageService = imageService;
	}

	[HttpGet("{id}")]
	[ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
	public async Task<IActionResult> Get(Guid id)
	{
		var image = await _imageService.GetImageByIdAsync(id);
		return File(image.Data, image.ContentType);
	}
}
