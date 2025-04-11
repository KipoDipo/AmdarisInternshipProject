using HiFive.Application.DTOs.Song;

namespace HiFive.Application.Services.Contracts;
public interface ISongService
{
	Task<SongDto> CreateSongAsync(string title, Guid artistId, TimeSpan duration);
	Task<SongDto> GetSongByIdAsync(Guid songId);
	Task<SongDto> GetSongByPartialName(string partialName);

	Task<SongDetailsDto> GetSongDetailsByIdAsync(Guid songId);
	Task<SongDto> UpdateSongAsync(Guid songId, string title, Guid artistId);

	Task<IEnumerable<SongDto>> GetSongsByArtistIdAsync(Guid artistId);
}
