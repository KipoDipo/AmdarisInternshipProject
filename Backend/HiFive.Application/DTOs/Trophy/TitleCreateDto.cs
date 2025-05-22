namespace HiFive.Application.DTOs.Trophy;

public class TitleCreateDto
{
	public required string Name { get; set; }
	public required string Description { get; set; }

	public Guid ConditionId { get; set; }

	public Guid? ArtistId { get; set; }
}
