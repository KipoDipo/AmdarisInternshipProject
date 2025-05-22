using HiFive.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiFive.Infrastructure.Db.Configurations;

public class ListenerConfiguration : IEntityTypeConfiguration<Listener>
{
	public void Configure(EntityTypeBuilder<Listener> builder)
	{
		builder
			.HasMany(l => l.Badges)
			.WithMany(b => b.Owners);

		builder
			.HasOne(l => l.EquippedBadge);

		builder
			.HasMany(l => l.Titles)
			.WithMany(t => t.Owners);

		builder
			.HasOne(l => l.EquippedTitle);
	}
}
