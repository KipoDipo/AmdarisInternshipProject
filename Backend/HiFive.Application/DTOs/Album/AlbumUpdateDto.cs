namespace HiFive.Application.DTOs.Album;

public class AlbumUpdateDto
{
	public Guid Id { get; set; }

	public string? Title { get; set; }

	public string? Description { get; set; }
}
