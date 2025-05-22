using HiFive.Domain.Contracts;

namespace HiFive.Application.Contracts.Repositories.Helpers;

public interface IRegisterable<T, Dto> where T : User
{
	Task<T> Register(Dto dto);
}
