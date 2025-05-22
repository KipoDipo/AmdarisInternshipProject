using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Users;

namespace HiFive.Domain.Models.Join;

public class ListenerLikedSong
{
	public int Id { get; set; }

	public Guid ListenerId { get; set; }
	public required Listener Listener { get; set; }

	public Guid LikedSongId { get; set; }
	public required Song LikedSong { get; set; }

	public int OrderIndex { get; set; }
}
