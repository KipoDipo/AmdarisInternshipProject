namespace HiFive.Domain.Contracts;

public interface IDeletable : IBase
{
	bool IsDeleted { get; set; }
	DateTime? DeletedOn { get; set; }
}
