using HiFive.Domain.Contracts;

namespace HiFive.Domain.Models.Music;

public class Genre : IDeletable
{
	public Guid Id { get; set; }

	public required string Name { get; set; }

	public bool IsDeleted { get; set; }
	public DateTime? DeletedOn { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
