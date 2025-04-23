using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
{
	public AlbumRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public override async Task<Album?> GetWithDetailsByIdAsync(Guid id)
	{
		return await _dbContext.Set<Album>()
			.Include(a => a.Artist)
			.Include(a => a.Songs)
			.FirstOrDefaultAsync(a => a.Id == id);
	}
}
