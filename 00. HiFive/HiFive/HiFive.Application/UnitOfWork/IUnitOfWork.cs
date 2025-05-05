using HiFive.Application.Contracts.Repositories;

namespace HiFive.Application.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
	IListenerRepository Listeners { get; }
	IArtistRepository Artists { get; }
	IDistributorRepository Distributors { get; }
	IAdminRepository Admins { get; }

	IPlaylistRepository Playlists { get; }
	ISongRepository Songs { get; }
	IGenreRepository Genres { get; }
	IAlbumRepository Albums { get; }

	IBadgeRepository Badges { get; }
	ITitleRepository Titles { get; }

	IImageFileRepository Images { get; }

	Task BeginTransactionAsync();
	Task CommitTransactionAsync();
	Task RollbackTransactionAsync();
}
