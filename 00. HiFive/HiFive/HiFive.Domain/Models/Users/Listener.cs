using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Throphies;

namespace HiFive.Domain.Models.Users;

public class Listener : ApplicationUser
{
	public List<Artist> FollowingArtists { get; set; } = [];
	public List<Listener> FollowingListeners { get; set; } = [];

	public List<Playlist> CreatedPlaylists { get; set; } = [];
	
	public List<Playlist> LikedPlaylists { get; set; } = [];
	public List<Song> LikedSongs{ get; set; } = [];

	public List<Badge> Badges { get; set; } = [];
	public List<Title> Titles { get; set; } = [];

	public Guid? EquippedBadgeId { get; set; }
	public Badge? EquippedBadge { get; set; }

	public Guid? EquippedTitleId { get; set; }
	public Title? EquippedTitle { get; set; }

	public bool IsSubscribed { get; set; } = false;
	public DateTime? SubscriptionEndDate { get; set; } = null;
}
