﻿using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Throphies;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class BadgeRepository : BaseRepository<Badge>, IBadgeRepository
{
	public BadgeRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public async Task<Badge?> GetWithDetailsByIdAsync(Guid id)
	{
		return await _dbContext.Set<Badge>()
			.Include(b => b.Artist)
			.Include(b => b.Condition)
			.FirstOrDefaultAsync(b => b.Id == id);
	}
}
