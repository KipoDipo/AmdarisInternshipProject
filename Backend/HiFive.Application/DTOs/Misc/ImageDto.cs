using HiFive.Domain.Models.Misc;

namespace HiFive.Application.DTOs.Misc;

public class ImageDto
{
	public required Guid Id { get; set; }
	public required string ContentType { get; set; }
	public required byte[] Data { get; set; }

	public static ImageDto FromEntity(ImageFile image)
	{
		return new ImageDto
		{
			Id = image.Id,
			ContentType = image.ContentType,
			Data = image.Data
		};
	}
}
