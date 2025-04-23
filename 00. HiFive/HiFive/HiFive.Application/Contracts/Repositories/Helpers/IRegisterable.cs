using HiFive.Domain.Contracts;

namespace HiFive.Application.Contracts.Repositories.Helpers;

public interface IRegisterable<T> where T : IBase
{
	Task Register(T entity, string password);
}
