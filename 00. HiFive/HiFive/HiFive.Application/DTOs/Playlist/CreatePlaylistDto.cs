namespace HiFive.Application.DTOs.Playlist;
public class CreatePlaylistDto
{
	public required string Title { get; set; }
	public string? Description { get; set; }
	public required Guid OwnerId { get; set; }
}
