namespace HiFive.Application.DTOs.Misc;

public class ImageCreateDto
{
	public required string ContentType { get; set; }
	public required byte[] Data { get; set; }
}
