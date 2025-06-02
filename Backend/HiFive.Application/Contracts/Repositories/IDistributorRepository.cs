using HiFive.Application.Contracts.Repositories.Helpers;
using HiFive.Application.DTOs.Distributor;
using HiFive.Domain.Models.Users;

namespace HiFive.Application.Contracts.Repositories;

public interface IDistributorRepository : IRepository<Distributor>, IGettableWithDetails<Distributor>, IRegisterable<Distributor, DistributorCreateDto>, IGettableByPartialName<Distributor>
{
}
