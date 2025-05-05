using HiFive.Application.DTOs.Artist;

namespace HiFive.Presentation.Controllers.Requests.Users;

public class ArtistUpdateRequest
{
	public Guid Id { get; set; }

	public string? DisplayName { get; set; }
	public string? Bio { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public IFormFile? ProfilePicture { get; set; }

	public ArtistUpdateDto ToAristUpdateDto()
	{
		return new ArtistUpdateDto
		{
			Id = Id,
			DisplayName = DisplayName,
			Bio = Bio,
			FirstName = FirstName,
			LastName = LastName,
		};
	}
}
