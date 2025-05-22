using HiFive.Domain.Contracts;

namespace HiFive.Domain.Models.Misc;

public class ImageFile : IDeletable
{
	public Guid Id { get; set; }

	public required string ContentType { get; set; }
	public required byte[] Data { get; set; }

	public bool IsDeleted { get; set; }
	public DateTime? DeletedOn { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
