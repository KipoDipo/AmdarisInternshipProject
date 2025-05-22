using HiFive.Domain.Models.Misc;

namespace HiFive.Domain.Contracts;

public abstract class User : IDeletable
{
	public Guid Id { get; set; }

	public string DisplayName { get; set; } = null!;
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Bio { get; set; }

	public Guid? ProfilePictureId { get; set; }
	public ImageFile? ProfilePicture { get; set; }

	public bool IsDeleted { get; set; }
	public DateTime? DeletedOn { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}