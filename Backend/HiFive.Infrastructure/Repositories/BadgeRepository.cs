using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Throphies;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class BadgeRepository : BaseRepository<Badge>, IBadgeRepository
{
	public BadgeRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public override async Task<Badge?> GetWithDetailsByIdAsync(Guid id)
	{
		return await _dbContext.Set<Badge>()
			.Include(b => b.Description)
			.FirstOrDefaultAsync(b => b.Id == id);
	}
}
