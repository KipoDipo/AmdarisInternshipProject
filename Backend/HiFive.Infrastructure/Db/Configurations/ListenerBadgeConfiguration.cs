using HiFive.Domain.Models.Join;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiFive.Infrastructure.Db.Configurations;
public class ListenerBadgeConfiguration : IEntityTypeConfiguration<ListenerBadge>
{
	public void Configure(EntityTypeBuilder<ListenerBadge> builder)
	{
		builder
			.HasOne(lb => lb.Listener)
			.WithMany(l => l.Badges);

		builder
			.HasOne(lb => lb.Badge)
			.WithMany(b => b.Owners);
	}
}
