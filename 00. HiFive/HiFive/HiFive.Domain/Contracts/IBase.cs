namespace HiFive.Domain.Contracts;

public interface IBase
{
	Guid Id { get; set; }
	DateTime CreatedAt { get; set; }
	DateTime UpdatedAt { get; set; }
}
