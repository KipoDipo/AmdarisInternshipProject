using HiFive.Application.DTOs.Album;

namespace HiFive.Application.Contracts.Services.Contracts;

public interface IAlbumService
{
	Task<AlbumDto> CreateAlbumAsync(AlbumCreateDto albumCreateDto);
	Task<AlbumDto> GetAlbumByIdAsync(Guid albumId);
	Task<AlbumDetailsDto> GetAlbumDetailsByIdAsync(Guid albumId);
	Task<IEnumerable<AlbumDto>> GetAllAlbumsByArtistAsync(Guid artistId);
	Task<IEnumerable<AlbumDto>> GetAllAlbumsByPartialTitleAsync(string partialTitle);
	Task AddSongToAlbumAsync(Guid albumId, Guid songId);
	Task UpdateAlbumAsync(AlbumUpdateDto albumUpdateDto);
	Task DeleteAlbumAsync(Guid albumId);
}
