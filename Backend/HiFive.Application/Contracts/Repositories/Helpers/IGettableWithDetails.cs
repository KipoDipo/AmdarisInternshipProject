namespace HiFive.Application.Contracts.Repositories.Helpers;
public interface IGettableWithDetails<T>
{
	Task<T?> GetWithDetailsByIdAsync(Guid id);
}
