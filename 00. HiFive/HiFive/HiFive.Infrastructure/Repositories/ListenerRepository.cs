using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HiFive.Infrastructure.Repositories;
public class ListenerRepository : BaseRepository<Listener>, IListenerRepository
{
	private readonly BaseUserManager<Listener> _listenerManager;

	public ListenerRepository(DbContext dbContext, BaseUserManager<Listener> userManager) : base(dbContext)
	{
		_listenerManager = userManager;
	}

	public async Task<IEnumerable<Listener>> GetAllByPartialName(string partialName)
	{
		return await _dbContext.Set<Listener>()
			.Where(x => x.DisplayName.Contains(partialName))
			.ToListAsync();
	}

	public override async Task<Listener?> GetWithDetailsByIdAsync(Guid id)
	{
		return await _dbContext.Set<Listener>()
			.Include(l => l.CreatedPlaylists)
			.Include(l => l.Badges)
			.Include(l => l.Titles)
			.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task Register(Listener entity, string password)
	{
		await _listenerManager.CreateAsync(entity, password);
	}
}
