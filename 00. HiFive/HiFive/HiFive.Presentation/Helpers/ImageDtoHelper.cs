using HiFive.Application.DTOs.Misc;

namespace HiFive.Presentation.Extentions;

public static class ImageDtoHelper
{
	public static ImageCreateDto CreateDtoFromFormFile(IFormFile file)
	{
		var image = new ImageCreateDto
		{
			ContentType = file.ContentType,
			Data = new byte[file.Length]
		};
		using (var stream = file.OpenReadStream())
		{
			stream.Read(image.Data, 0, (int)file.Length);
		}
		return image;
	}

	public static ImageUpdateDto UpdateDtoFromFormFile(IFormFile file, Guid id)
	{
		var image = new ImageUpdateDto
		{
			Id = id,
			ContentType = file.ContentType,
			Data = new byte[file.Length]
		};
		using (var stream = file.OpenReadStream())
		{
			stream.Read(image.Data, 0, (int)file.Length);
		}
		return image;
	}
}
