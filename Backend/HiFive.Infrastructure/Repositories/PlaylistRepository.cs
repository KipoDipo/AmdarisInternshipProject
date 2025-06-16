using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class PlaylistRepository : BaseRepository<Playlist>, IPlaylistRepository
{
	public PlaylistRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public async Task<Playlist?> GetWithDetailsByIdAsync(Guid id)
	{
		return await _dbContext.Set<Playlist>()
			.Include(p => p.Songs)
				.ThenInclude(s => s.Song)
					.ThenInclude(s => s.Artist)
			.Include(p => p.Songs)
			.ThenInclude(s => s.Song)
				.ThenInclude(s => s.AlbumSong)
					.ThenInclude(s => s.Album)

			.FirstOrDefaultAsync(p => p.Id == id);
	}
}
