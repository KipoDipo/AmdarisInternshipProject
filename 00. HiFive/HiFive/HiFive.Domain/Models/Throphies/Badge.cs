using HiFive.Domain.Contracts;

namespace HiFive.Domain.Models.Throphies;

public class Badge : IDeletable
{
	public Guid Id { get; set; }

	public required string Name { get; set; }
	public required string Description { get; set; }
	public byte[]? Image { get; set; }

	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
