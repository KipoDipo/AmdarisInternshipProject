using HiFive.Domain.Models.Throphies;
using HiFive.Domain.Models.Users;

namespace HiFive.Domain.Models.Join;
public class ListenerBadge
{
	public Guid Id { get; set; }

	public Guid ListenerId { get; set; }
	public Listener Listener { get; set; } = null!;

	public Guid BadgeId { get; set; }
	public Badge Badge { get; set; } = null!;

	public DateTime AwardedAt { get; set; }
}
