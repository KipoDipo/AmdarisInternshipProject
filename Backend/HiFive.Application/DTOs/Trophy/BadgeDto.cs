using HiFive.Domain.Models.Throphies;

namespace HiFive.Application.DTOs.Trophy;

public class BadgeDto
{
	public Guid Id { get; set; }

	public required string Name { get; set; }
	public required string Description { get; set; }

	public Guid? ArtistId { get; set; }

	public Guid ImageId { get; set; }

	public static BadgeDto FromEntity(Badge badge)
	{
		return new()
		{
			Id = badge.Id,
			Name = badge.Name,
			Description = badge.Description,
			ArtistId = badge.ArtistId,
			ImageId = badge.ImageId,
		};
	}
}
