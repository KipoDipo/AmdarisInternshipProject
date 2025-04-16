using HiFive.Application.DTOs.Song;

namespace HiFive.Application.Services.Contracts;
public interface ISongService
{
	Task<SongDto> CreateSongAsync(string title, Guid artistId, Guid? albumId, uint duration,
		List<Guid> genreIds, DateTime releaseDate, string data); // TODO: Refactor

	Task<SongDto> GetSongByIdAsync(Guid songId);
	Task<IEnumerable<SongDto>> GetSongsByPartialNameAsync(string partialName);
	Task<IEnumerable<SongDto>> GetAllSongsByGenreAsync(Guid genreId);

	Task<SongDetailsDto> GetSongDetailsByIdAsync(Guid songId);
	Task UpdateSongAsync(Guid songId, string title, DateTime releaseDate); // TODO: Refactor


	Task<IEnumerable<SongDto>> GetListenerLikedSongs(Guid listenerId);
	Task<IEnumerable<SongDto>> GetSongsByArtistIdAsync(Guid artistId);
}
