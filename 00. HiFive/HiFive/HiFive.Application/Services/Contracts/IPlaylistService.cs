using HiFive.Application.DTOs.Playlist;
using HiFive.Application.DTOs.Song;

namespace HiFive.Application.Services.Contracts;

public interface IPlaylistService
{
	Task<PlaylistDto> CreatePlaylistAsync(PlaylistCreateDto playlistCreateDto);
	Task<PlaylistDto> GetPlaylistByIdAsync(Guid playlistId);
	Task<PlaylistDetailsDto> GetPlaylistDetailsByIdAsync(Guid playlistId);
	Task<IEnumerable<PlaylistDto>> GetPlaylistsByUserIdAsync(Guid userId);

	Task UpdatePlaylistAsync(Guid playlistId, string name, string description);
	Task DeletePlaylistAsync(Guid playlistId);

	Task AddSongToPlaylistAsync(Guid playlistId, Guid songId);
	Task RemoveSongFromPlaylistAsync(Guid playlistId, Guid songId);
	Task<IEnumerable<SongDto>> GetSongsInPlaylistAsync(Guid playlistId);
}
