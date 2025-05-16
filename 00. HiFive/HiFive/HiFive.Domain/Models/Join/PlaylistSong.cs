using HiFive.Domain.Models.Music;

namespace HiFive.Domain.Models.Join;

public class PlaylistSong
{
	public Guid Id { get; set; }
	
	public Guid PlaylistId { get; set; }
	public required Playlist Playlist { get; set; }

	public Guid SongId { get; set; }
	public required Song Song { get; set; }

	public required int OrderIndex { get; set; }
}
