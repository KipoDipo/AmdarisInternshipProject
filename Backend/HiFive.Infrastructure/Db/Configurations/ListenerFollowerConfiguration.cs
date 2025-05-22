using HiFive.Domain.Models.Join;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiFive.Infrastructure.Db.Configurations;

public class ListenerFollowerConfiguration : IEntityTypeConfiguration<ListenerFollower>
{
	public void Configure(EntityTypeBuilder<ListenerFollower> builder)
	{
		builder
			.HasKey(lf => new { lf.FollowerId, lf.FollowedId });

		builder // Listener that follows
			.HasOne(lf => lf.Follower)
			.WithMany(l => l.FollowingListeners)
			.OnDelete(DeleteBehavior.NoAction);

		builder // Listener that is being followed
			.HasOne(lf => lf.Followed)
			.WithMany(l => l.FollowedByListeners)
			.OnDelete(DeleteBehavior.NoAction);

	}
}
