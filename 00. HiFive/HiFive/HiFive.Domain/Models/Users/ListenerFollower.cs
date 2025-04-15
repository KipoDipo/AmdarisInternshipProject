namespace HiFive.Domain.Models.Users;
public class ListenerFollower
{
	public Guid FollowerId { get; set; }
	public Listener Follower { get; set; } = null!;

	public Guid FollowedId { get; set; }
	public Listener Followed { get; set; } = null!;
}
