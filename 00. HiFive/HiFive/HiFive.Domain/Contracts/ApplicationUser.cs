using Microsoft.AspNetCore.Identity;

namespace HiFive.Domain.Contracts;

public abstract class ApplicationUser : IdentityUser<Guid>, IDeletable
{
	public required string FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Bio { get; set; }
	public byte[]? ProfilePicture { get; set; }

	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}