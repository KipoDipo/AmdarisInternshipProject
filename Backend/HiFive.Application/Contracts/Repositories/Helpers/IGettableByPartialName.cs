using HiFive.Domain.Contracts;

namespace HiFive.Application.Contracts.Repositories.Helpers;

public interface IGettableByPartialName<T> where T : IBase
{
	Task<IEnumerable<T>> GetAllByPartialName(string partialName);
}
