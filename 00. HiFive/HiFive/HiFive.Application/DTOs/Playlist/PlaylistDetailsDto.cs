namespace HiFive.Application.DTOs.Playlist;

public class PlaylistDetailsDto
{
	public Guid Id { get; set; }

	public required string Title { get; set; }

	public string? Description { get; set; }

	public Guid OwnerId { get; set; }

	public static PlaylistDetailsDto FromEntity(Domain.Models.Music.Playlist playlist)
	{
		return new PlaylistDetailsDto
		{
			Id = playlist.Id,
			Title = playlist.Title,
			Description = playlist.Description,
			OwnerId = playlist.OwnerId,
		};
	}

}
