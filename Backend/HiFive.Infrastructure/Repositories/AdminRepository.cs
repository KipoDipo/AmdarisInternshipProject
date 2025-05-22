using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;
public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
	public AdminRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public override Task<Admin?> GetWithDetailsByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<Admin> Register(int dto)
	{
		throw new NotImplementedException();
	}
}
