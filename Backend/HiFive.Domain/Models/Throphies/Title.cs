using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Users;

namespace HiFive.Domain.Models.Throphies;

public class Title : IDeletable
{
	public Guid Id { get; set; }

	public required string Name { get; set; }
	public required string Description { get; set; }

	public Guid ConditionId { get; set; }
	public Condition Condition { get; set; } = null!;

	public Guid? ArtistId { get; set; }
	public Artist? Artist { get; set; }

	public List<Listener> Owners { get; set; } = [];

	public bool IsDeleted { get; set; }
	public DateTime? DeletedOn { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
