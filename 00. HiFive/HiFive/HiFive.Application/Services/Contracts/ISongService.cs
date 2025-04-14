using HiFive.Application.DTOs.Song;
using HiFive.Domain.Models.Music;

namespace HiFive.Application.Services.Contracts;
public interface ISongService
{
	Task<SongDto> CreateSongAsync(string title, Guid artistId, TimeSpan duration, 
		List<Genre> genres, DateTime releaseDate, byte[] data);
	Task<SongDto> GetSongByIdAsync(Guid songId);
	Task<IEnumerable<SongDto>> GetSongsByPartialName(string partialName);

	Task<SongDetailsDto> GetSongDetailsByIdAsync(Guid songId);
	Task<SongDto> UpdateSongAsync(Guid songId, string title, DateTime releaseDate);

	Task<IEnumerable<SongDto>> GetSongsByArtistIdAsync(Guid artistId);
}
