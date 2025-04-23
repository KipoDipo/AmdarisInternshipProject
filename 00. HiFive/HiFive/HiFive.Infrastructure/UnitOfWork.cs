using HiFive.Application.Contracts.Repositories;
using HiFive.Application.UnitOfWork;
using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Users;
using HiFive.Infrastructure.Db;
using HiFive.Infrastructure.Repositories;

namespace HiFive.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
	private ApplicationDbContext _context;

	public IListenerRepository Listeners { get; }
	public IArtistRepository Artists { get; }
	public IDistributorRepository Distributors { get; }
	public IAdminRepository Admins { get; }
	public IPlaylistRepository Playlists { get; }
	public ISongRepository Songs { get; }
	public IGenreRepository Genres { get; }
	public IAlbumRepository Albums { get; }
	public IBadgeRepository Badges { get; }
	public ITitleRepository Titles { get; }

	public UnitOfWork(ApplicationDbContext dbContext, BaseUserManager<Listener> _listenerManager, BaseUserManager<Artist> _artistManager)
	{
		_context = dbContext;
		Listeners = new ListenerRepository(dbContext, _listenerManager);
		Artists = new ArtistRepository(dbContext, _artistManager);
		Distributors = new DistributorRepository(dbContext);
		Admins = new AdminRepository(dbContext);
		Playlists = new PlaylistRepository(dbContext);
		Songs = new SongRepository(dbContext);
		Genres = new GenreRepository(dbContext);
		Albums = new AlbumRepository(dbContext);
		Badges = new BadgeRepository(dbContext);
		Titles = new TitleRepository(dbContext);
	}

	public async Task BeginTransactionAsync()
	{
		await _context.Database.BeginTransactionAsync();
	}

	public async Task CommitTransactionAsync()
	{
		await _context.Database.CommitTransactionAsync();
		await _context.SaveChangesAsync();
	}

	public async Task RollbackTransactionAsync()
	{
		await _context.Database.RollbackTransactionAsync();
	}

	public void Dispose()
	{
		_context?.Dispose();
		GC.SuppressFinalize(this);
	}
}
