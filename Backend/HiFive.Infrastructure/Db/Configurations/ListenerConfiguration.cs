using HiFive.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiFive.Infrastructure.Db.Configurations;

public class ListenerConfiguration : IEntityTypeConfiguration<Listener>
{
	public void Configure(EntityTypeBuilder<Listener> builder)
	{
		builder
			.HasOne(l => l.EquippedBadge);

		builder
			.HasOne(l => l.EquippedTitle);
	}
}
