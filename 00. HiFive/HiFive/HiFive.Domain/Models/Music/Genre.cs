using HiFive.Domain.Contracts;

namespace HiFive.Domain.Models.Music;

public class Genre : IDeletable
{
	public Guid Id { get; set; }

	public required string Name { get; set; }

	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
