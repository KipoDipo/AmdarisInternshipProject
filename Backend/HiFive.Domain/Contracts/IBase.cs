namespace HiFive.Domain.Contracts;

public interface IBase
{
	Guid Id { get; set; }
	DateTime CreatedOn { get; set; }
	DateTime UpdatedOn { get; set; }
}
