namespace HiFive.Application.DTOs.Misc;

public class ImageUpdateDto
{
	public required Guid Id { get; set; }
	public required string ContentType { get; set; }
	public required byte[] Data { get; set; }
}
