namespace HiFive.Application.DTOs.Playlist;

public class PlaylistCreateDto
{
	public required string Title { get; set; }

	public string? Description { get; set; }

	public Guid? ThumbnailId { get; set; }

	public Guid OwnerId { get; set; }
}
