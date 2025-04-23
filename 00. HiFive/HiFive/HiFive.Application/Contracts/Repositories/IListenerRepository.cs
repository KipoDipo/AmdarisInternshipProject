using HiFive.Application.Contracts.Repositories.Helpers;
using HiFive.Domain.Models.Users;

namespace HiFive.Application.Contracts.Repositories;

public interface IListenerRepository : IRepository<Listener>, IRegisterable<Listener>, IGettableByPartialName<Listener>
{
}
