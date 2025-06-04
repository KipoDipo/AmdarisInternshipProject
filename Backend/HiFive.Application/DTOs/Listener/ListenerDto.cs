namespace HiFive.Application.DTOs.Listener;

public class ListenerDto
{
	public Guid Id { get; set; }

	public required string DisplayName { get; set; }

	public Guid? ProfilePictureId { get; set; }

	public Guid? BadgeId { get; set; }
	public Guid? TitleId { get; set; }

	public bool IsSubscribed { get; set; }

	public static ListenerDto FromEntity(Domain.Models.Users.Listener listener)
	{
		return new ListenerDto
		{
			Id = listener.Id,
			DisplayName = listener.DisplayName,
			ProfilePictureId = listener.ProfilePictureId,
			BadgeId = listener.EquippedBadgeId,
			TitleId = listener.EquippedTitleId,
			IsSubscribed = listener.IsSubscribed,
		};
	}
}
