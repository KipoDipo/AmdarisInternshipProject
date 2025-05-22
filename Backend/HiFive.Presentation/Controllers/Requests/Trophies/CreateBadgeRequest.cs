using HiFive.Application.DTOs.Trophy;

namespace HiFive.Presentation.Controllers.Requests.Trophies;

public class CreateBadgeRequest
{
	public required string Name { get; set; }
	public required string Description { get; set; }

	public Guid ConditionId { get; set; }

	public Guid? ArtistId { get; set; }

	public required IFormFile Image { get; set; }

	public BadgeCreateDto ToBadgeCreateDto(Guid imageId)
	{
		return new()
		{
			Name = Name,
			ArtistId = ArtistId,
			ConditionId = ConditionId,
			Description = Description,
			ImageId = imageId
		};
	}
}
