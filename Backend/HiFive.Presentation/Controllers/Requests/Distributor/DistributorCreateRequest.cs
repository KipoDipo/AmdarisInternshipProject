using HiFive.Application.DTOs.Distributor;

namespace HiFive.Presentation.Controllers.Requests.Distributor;

public class DistributorCreateRequest
{
	public required string UserName { get; set; }
	public required string DisplayName { get; set; }
	public required string Email { get; set; }
	public required string Password { get; set; }

	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Bio { get; set; }
	public IFormFile? ProfilePicture { get; set; }
	public string? PhoneNumber { get; set; }

	public DistributorCreateDto ToDistributorCreateDto(Guid? profilePictureId)
	{
		return new DistributorCreateDto
		{
			UserName = UserName,
			DisplayName = DisplayName,
			Email = Email,
			Password = Password,
			FirstName = FirstName,
			LastName = LastName,
			Bio = Bio,
			ProfilePictureId = profilePictureId,
			PhoneNumber = PhoneNumber
		};
	}
}
