namespace HiFive.Application.DTOs.Listener;

public class ListenerUpdateMeRequest
{
	public string? DisplayName { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Bio { get; set; }
	public string? PhoneNumber { get; set; }
	public IFormFile? ProfilePicture { get; set; }
	public Guid? EquippedBadgeId { get; set; }
	public Guid? EquippedTitleId { get; set; }

	public ListenerUpdateDto ToListenerUpdateDto(Guid listenerId, Guid? profilePictureId)
	{
		return new ListenerUpdateDto
		{
			Id = listenerId,
			DisplayName = DisplayName,
			FirstName = FirstName,
			LastName = LastName,
			Bio = Bio,
			ProfilePictureId = profilePictureId,
			PhoneNumber = PhoneNumber,
			EquippedBadgeId = EquippedBadgeId,
			EquippedTitleId = EquippedTitleId
		};
	}

}
