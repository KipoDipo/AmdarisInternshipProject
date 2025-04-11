using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Users;

namespace HiFive.Application.Contracts;

public interface IUnitOfWork : IDisposable
{
	IRepository<Playlist> Playlists { get; }
	IRepository<Song> Songs { get; }
	IRepository<Album> Albums { get; }
	IRepository<Listener> Listeners { get; }

	Task BeginTransactionAsync();
	Task CommitTransactionAsync();
	Task RollbackTransactionAsync();
}
