using HiFive.Domain.Models.Join;

namespace HiFive.Application.DTOs.Trophy;
public class ListenerBadgeDto
{
	public Guid Id { get; set; }

	public required string Name { get; set; }
	public required string Description { get; set; }

	public Guid? ArtistId { get; set; }

	public Guid ImageId { get; set; }

	public DateTime? AwardedOn { get; set; }

	public static ListenerBadgeDto FromEntity(ListenerBadge entity)
	{
		return new()
		{
			Id = entity.Id,
			Name = entity.Badge.Name,
			Description = entity.Badge.Description,
			ArtistId = entity.Badge.ArtistId,
			ImageId = entity.Badge.ImageId,
			AwardedOn = entity.AwardedAt
		};
	}
}
