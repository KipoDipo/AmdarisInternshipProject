using HiFive.Application.DTOs.Song;

namespace HiFive.Application.Services.Contracts;
public interface ISongService
{
	Task<SongDto> CreateSongAsync(string title, Guid artistId, TimeSpan duration,
		List<Guid> genreIds, DateTime releaseDate, byte[] data);

	Task<SongDto> GetSongByIdAsync(Guid songId);
	Task<IEnumerable<SongDto>> GetSongsByPartialNameAsync(string partialName);
	Task<IEnumerable<SongDto>> GetAllSongsByGenreAsync(Guid genreId);

	Task<SongDetailsDto> GetSongDetailsByIdAsync(Guid songId);
	Task<SongDto> UpdateSongAsync(Guid songId, string title, DateTime releaseDate);


	Task<IEnumerable<SongDto>> GetSongsByArtistIdAsync(Guid artistId);
}
