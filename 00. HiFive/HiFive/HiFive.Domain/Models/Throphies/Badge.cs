using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Misc;
using HiFive.Domain.Models.Users;

namespace HiFive.Domain.Models.Throphies;

public class Badge : IDeletable
{
	public Guid Id { get; set; }

	public required string Name { get; set; }
	public required string Description { get; set; }

	public Guid ImageId { get; set; }
	public required ImageFile Image { get; set; }

	public List<Listener> Owners { get; set; } = [];

	public bool IsDeleted { get; set; }
	public DateTime? DeletedOn { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
