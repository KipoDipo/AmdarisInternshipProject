using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Users;

namespace HiFive.Application.DTOs.Playlist;

public class PlaylistDto
{
	public Guid Id { get; set; }

	public required string Title { get; set; }

	public string? Description { get; set; }

	public Guid OwnerId { get; set; }

	public static PlaylistDto FromEntity(Domain.Models.Music.Playlist playlist)
	{
		return new PlaylistDto
		{
			Id = playlist.Id,
			Title = playlist.Title,
			Description = playlist.Description,
			OwnerId = playlist.OwnerId,
		};
	}
}
