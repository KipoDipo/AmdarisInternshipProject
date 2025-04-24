using HiFive.Application.Contracts.Repositories.Helpers;
using HiFive.Domain.Models.Users;

namespace HiFive.Application.Contracts.Repositories;

public interface IDistributorRepository : IRepository<Distributor>, IRegisterable<Distributor, int>
{
}
