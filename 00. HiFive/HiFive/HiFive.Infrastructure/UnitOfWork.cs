using HiFive.Application.Contracts;
using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Throphies;
using HiFive.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
	public required DbContext Context { get; init; }

	public IRepository<Listener> Listeners { get; }
	public IRepository<Artist> Artists { get; }
	public IRepository<Distributor> Distributors { get; }
	public IRepository<Admin> Admins { get; }
	public IRepository<Playlist> Playlists { get; }
	public IRepository<Song> Songs { get; }
	public IRepository<Genre> Genres { get; }
	public IRepository<Album> Albums { get; }
	public IRepository<Badge> Badges { get; }
	public IRepository<Title> Titles { get; }

	public UnitOfWork(DbContext dbContext)
	{
		Context = dbContext;
		Listeners = new Repository<Listener>(dbContext);
		Artists = new Repository<Artist>(dbContext);
		Distributors = new Repository<Distributor>(dbContext);
		Admins = new Repository<Admin>(dbContext);
		Playlists = new Repository<Playlist>(dbContext);
		Songs = new Repository<Song>(dbContext);
		Genres = new Repository<Genre>(dbContext);
		Albums = new Repository<Album>(dbContext);
		Badges = new Repository<Badge>(dbContext);
		Titles = new Repository<Title>(dbContext);
	}

	public async Task BeginTransactionAsync()
	{
		await Context.Database.BeginTransactionAsync();
	}

	public async Task CommitTransactionAsync()
	{
		await Context.Database.CommitTransactionAsync();
		await Context.SaveChangesAsync();
	}

	public async Task RollbackTransactionAsync()
	{
		await Context.Database.RollbackTransactionAsync();
	}

	public void Dispose()
	{
		Context?.Dispose();
	}
}
