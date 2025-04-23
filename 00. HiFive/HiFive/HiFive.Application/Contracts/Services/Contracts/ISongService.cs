using HiFive.Application.DTOs.Song;

namespace HiFive.Application.Contracts.Services.Contracts;
public interface ISongService
{
	Task<SongDto> CreateSongAsync(SongCreateDto songCreateDto);

	Task<SongDto> GetSongByIdAsync(Guid songId);
	Task<IEnumerable<SongDto>> GetSongsByPartialNameAsync(string partialName);
	Task<IEnumerable<SongDto>> GetAllSongsByGenreAsync(Guid genreId);
	Task<IEnumerable<SongDto>> GetAllSongsByPlaylistIdAsync(Guid playlistId);
	Task<IEnumerable<SongDto>> GetAllSongsByAlbumIdAsync(Guid albumId);

	Task<SongDetailsDto> GetSongDetailsByIdAsync(Guid songId);
	Task UpdateSongAsync(SongUpdateDto songUpdateDto);


	Task<IEnumerable<SongDto>> GetListenerLikedSongs(Guid listenerId);
	Task<IEnumerable<SongDto>> GetSongsByArtistIdAsync(Guid artistId);
}
