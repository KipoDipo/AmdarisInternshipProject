using HiFive.Domain.Models.Music;

namespace HiFive.Domain.Models.Join;

public class AlbumSong
{
	public Guid Id { get; set; }

	public Guid AlbumId { get; set; }
	public required Album Album { get; set; }

	public Guid SongId { get; set; }
	public required Song Song { get; set; }

	public required int OrderIndex { get; set; }
}
