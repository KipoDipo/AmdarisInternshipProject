namespace HiFive.Application.DTOs.Listener;

public class ListenerDetailsDto
{
	public Guid Id { get; set; }

	public required string DisplayName { get; set; }

	public string? Bio { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public Guid? ProfilePictureId { get; set; }

	public List<Guid> CreatedPlaylistsIds { get; set; } = [];

	public Guid? EquippedBadgeId { get; set; }
	public List<Guid> BadgeIds { get; set; } = [];

	public Guid? EquippedTitleId { get; set; }
	public List<Guid> TitleIds { get; set; } = [];


	public static ListenerDetailsDto FromEntity(Domain.Models.Users.Listener listener)
	{
		return new ListenerDetailsDto
		{
			Id = listener.Id,
			DisplayName = listener.DisplayName,
			Bio = listener.Bio,
			FirstName = listener.FirstName,
			LastName = listener.LastName,
			ProfilePictureId = listener.ProfilePictureId,
			CreatedPlaylistsIds = listener.CreatedPlaylists.Select(p => p.Id).ToList(),
			EquippedBadgeId = listener.EquippedBadgeId,
			BadgeIds = listener.Badges.Select(b => b.Id).ToList(),
			EquippedTitleId = listener.EquippedTitleId,
			TitleIds = listener.Titles.Select(t => t.Id).ToList()
		};
	}
}
