namespace HiFive.Domain.Contracts;

public interface IDeletable : IBase
{
	bool IsDeleted { get; set; }
	DateTime? DeletedAt { get; set; }
}
