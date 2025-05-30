﻿using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
{
	public AlbumRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public async Task<Album?> GetWithDetailsByIdAsync(Guid id)
	{
		return await _dbContext.Set<Album>()
			.Include(a => a.Artist)
			.Include(a => a.Songs)
			.ThenInclude(s => s.Song)
			.FirstOrDefaultAsync(a => a.Id == id);
	}
}
