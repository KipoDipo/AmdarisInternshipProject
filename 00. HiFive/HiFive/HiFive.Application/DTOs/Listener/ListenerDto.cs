namespace HiFive.Application.DTOs.Listener;

public class ListenerDto
{
	public Guid Id { get; set; }

	public required string DisplayName { get; set; }

	public byte[]? ProfilePicture { get; set; }

	public Guid? BadgeId { get; set; }
	public Guid? TitleId { get; set; }

	public static ListenerDto FromEntity(Domain.Models.Users.Listener listener)
	{
		return new ListenerDto
		{
			Id = listener.Id,
			DisplayName = listener.DisplayName,
			ProfilePicture = listener.ProfilePicture,
			BadgeId = listener.EquippedBadgeId,
			TitleId = listener.EquippedTitleId
		};
	}
}
