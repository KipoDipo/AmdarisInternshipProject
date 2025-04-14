using HiFive.Application.DTOs.Album;

namespace HiFive.Application.Services.Contracts;

public interface IAlbumService
{
	Task<AlbumDto> CreateAlbumAsync(string title, DateTime releaseDate, Guid artistId);
	Task<AlbumDto> GetAlbumByIdAsync(Guid albumId);
	Task<IEnumerable<AlbumDto>> GetAllAlbumsByArtistAsync(Guid artistId);
	Task<IEnumerable<AlbumDto>> GetAllAlbumsByPartialTitleAsync(string partialTitle);
	Task AddSongToAlbumAsync(Guid albumId, Guid songId);
	Task UpdateAlbumAsync(Guid albumId, string title, DateTime releaseDate);
	Task DeleteAlbumAsync(Guid albumId);
}
