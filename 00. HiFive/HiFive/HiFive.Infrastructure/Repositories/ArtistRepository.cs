using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
{
	private readonly BaseUserManager<Artist> _artistManager;


	public ArtistRepository(DbContext dbContext, BaseUserManager<Artist> artistManager) : base(dbContext)
	{
		_artistManager = artistManager;
	}

	public async Task<IEnumerable<Artist>> GetAllByPartialName(string partialName)
	{
		return await _dbContext.Set<Artist>()
			.Where(a => a.DisplayName.Contains(partialName))
			.ToListAsync();
	}

	public override async Task<Artist?> GetWithDetailsByIdAsync(Guid id)
	{
		return await _dbContext.Set<Artist>()
			.Include(a => a.Albums)
			.Include(a => a.Singles)
			.FirstOrDefaultAsync(a => a.Id == id);
	}

	public async Task Register(Artist entity, string password)
	{
		await _artistManager.CreateAsync(entity, password);
	}
}
