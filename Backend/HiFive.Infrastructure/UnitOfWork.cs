using HiFive.Application.Contracts.Repositories;
using HiFive.Application.UnitOfWork;
using HiFive.Infrastructure.Db;
using HiFive.Infrastructure.Identity;
using HiFive.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

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
	public IImageFileRepository Images { get; }
	public IConditionRepository Conditions { get; }
	public IListenerDataRepository ListenerData { get; }

	public UnitOfWork(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
	{
		_context = dbContext;
		Listeners = new ListenerRepository(dbContext, userManager, roleManager);
		Artists = new ArtistRepository(dbContext, userManager, roleManager);
		Distributors = new DistributorRepository(dbContext, userManager, roleManager);
		Admins = new AdminRepository(dbContext);
		Playlists = new PlaylistRepository(dbContext);
		Songs = new SongRepository(dbContext);
		Genres = new GenreRepository(dbContext);
		Albums = new AlbumRepository(dbContext);
		Badges = new BadgeRepository(dbContext);
		Titles = new TitleRepository(dbContext);
		Images = new ImageFileRepository(dbContext);
		Conditions = new ConditionRepository(dbContext);
		ListenerData = new ListenerDataRepository(dbContext);
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
