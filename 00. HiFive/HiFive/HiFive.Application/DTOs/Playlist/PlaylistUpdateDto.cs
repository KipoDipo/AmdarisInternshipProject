namespace HiFive.Application.DTOs.Playlist;

public class PlaylistUpdateDto
{
	public Guid Id { get; set; }

	public string? Title { get; set; }

	public string? Description { get; set; }
}
