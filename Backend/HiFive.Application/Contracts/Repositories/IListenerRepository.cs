using HiFive.Application.Contracts.Repositories.Helpers;
using HiFive.Application.DTOs.Listener;
using HiFive.Domain.Models.Users;

namespace HiFive.Application.Contracts.Repositories;

public interface IListenerRepository : IRepository<Listener>, IRegisterable<Listener, ListenerCreateDto>, IGettableWithDetails<Listener>, IGettableByPartialName<Listener>
{
}
