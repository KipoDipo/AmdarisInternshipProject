using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Throphies;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class TitleRepository : BaseRepository<Title>, ITitleRepository
{
	public TitleRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public async Task<Title?> GetWithDetailsByIdAsync(Guid id)
	{
		return await _dbContext.Set<Title>()
			.Include(b => b.Artist)
			.Include(b => b.Condition)
			.FirstOrDefaultAsync(b => b.Id == id);
	}
}
