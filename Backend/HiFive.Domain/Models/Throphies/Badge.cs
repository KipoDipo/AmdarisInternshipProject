using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Join;
using HiFive.Domain.Models.Misc;
using HiFive.Domain.Models.Users;

namespace HiFive.Domain.Models.Throphies;

public class Badge : IDeletable
{
	public Guid Id { get; set; }

	public required string Name { get; set; }
	public required string Description { get; set; }

	public Guid ConditionId { get; set; }
	public Condition Condition { get; set; } = null!;

	public Guid? ArtistId { get; set; }
	public Artist? Artist { get; set; }

	public Guid ImageId { get; set; }
	public ImageFile Image { get; set; } = null!;

	public List<ListenerBadge> Owners { get; set; } = [];

	public bool IsDeleted { get; set; }
	public DateTime? DeletedOn { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
