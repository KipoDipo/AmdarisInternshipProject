using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class SongRepository : BaseRepository<Song>, ISongRepository 
{
	public SongRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public async override Task<Song?> GetByIdAsync(Guid id)
	{
		return await _dbContext.Set<Song>()
			.Include(s => s.Artist)
			.Include(s => s.Genres)
			.Include(s => s.AlbumSong)
			.ThenInclude(a => a.Album)
			.FirstOrDefaultAsync(s => s.Id == id);
	}

	public async Task<IEnumerable<Song>> GetAllByPartialName(string partialName)
	{
		return await _dbContext.Set<Song>()
			.Where(s => s.Title.Contains(partialName))
			.Include(s => s.Artist)
			.ToListAsync();
	}

	public async Task<Song?> GetWithDetailsByIdAsync(Guid id)
	{
		return await _dbContext.Set<Song>()
			.Include(s => s.Artist)
			.Include(s => s.AlbumSong)
			.Include(s => s.Genres)
			.FirstOrDefaultAsync(s => s.Id == id);
	}
}
