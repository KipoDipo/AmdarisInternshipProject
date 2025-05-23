using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class DistributorRepository : BaseRepository<Distributor>, IDistributorRepository
{
	public DistributorRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public Task<Distributor?> GetWithDetailsByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<Distributor> Register(int dto)
	{
		throw new NotImplementedException();
	}
}
