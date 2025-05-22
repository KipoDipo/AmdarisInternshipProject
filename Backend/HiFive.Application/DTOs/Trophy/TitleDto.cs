using HiFive.Domain.Models.Throphies;

namespace HiFive.Application.DTOs.Trophy;

public class TitleDto
{
	public Guid Id { get; set; }

	public required string Name { get; set; }
	public required string Description { get; set; }

	public Guid? ArtistId { get; set; }

	public static TitleDto FromEntity(Title title)
	{
		return new()
		{
			Id = title.Id,
			Name = title.Name,
			Description = title.Description,
			ArtistId = title.ArtistId,
		};
	}
}
