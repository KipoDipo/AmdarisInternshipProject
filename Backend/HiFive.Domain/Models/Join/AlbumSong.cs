using HiFive.Domain.Models.Music;

namespace HiFive.Domain.Models.Join;

public class AlbumSong
{
	public Guid Id { get; set; }

	public Guid AlbumId { get; set; }
	public Album Album { get; set; } = null!;

	public Guid SongId { get; set; }
	public Song Song { get; set; } = null!;

	public required int OrderIndex { get; set; }
}
