using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Users;
using HiFive.Domain.Models.Throphies;

namespace HiFive.Application.Contracts;

public interface IUnitOfWork : IDisposable
{
	IRepository<Listener> Listeners { get; }
	IRepository<Artist> Artists { get; }
	IRepository<Distributor> Distributors { get; }
	IRepository<Admin> Admins { get; }

	IRepository<Playlist> Playlists { get; }
	IRepository<Song> Songs { get; }
	IRepository<Genre> Genres { get; }
	IRepository<Album> Albums { get; }

	IRepository<Badge> Badges { get; }
	IRepository<Title> Titles { get; }

	Task BeginTransactionAsync();
	Task CommitTransactionAsync();
	Task RollbackTransactionAsync();
}
