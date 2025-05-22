using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Join;
using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Throphies;

namespace HiFive.Domain.Models.Users;

public class Listener : User
{
	public List<Artist> FollowingArtists { get; set; } = [];

	public List<ListenerFollower> FollowingListeners { get; set; } = [];
	public List<ListenerFollower> FollowedByListeners { get; set; } = [];

	public List<Playlist> CreatedPlaylists { get; set; } = [];

	public List<Playlist> LikedPlaylists { get; set; } = [];
	public List<ListenerLikedSong> LikedSongs { get; set; } = [];

	public List<ListenerBadge> Badges { get; set; } = [];
	public List<ListenerTitle> Titles { get; set; } = [];

	public Guid? EquippedBadgeId { get; set; }
	public Badge? EquippedBadge { get; set; }

	public Guid? EquippedTitleId { get; set; }
	public Title? EquippedTitle { get; set; }

	public bool IsSubscribed { get; set; } = false;
	public DateTime? SubscriptionEndDate { get; set; } = null;
}
