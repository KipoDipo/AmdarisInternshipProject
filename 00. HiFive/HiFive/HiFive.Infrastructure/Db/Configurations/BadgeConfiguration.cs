using HiFive.Domain.Models.Throphies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiFive.Infrastructure.Db.Configurations;
public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
{
	public void Configure(EntityTypeBuilder<Badge> builder)
	{
		builder
			.Property(b => b.Name)
			.HasMaxLength(32);

		builder
			.Property(b => b.Description)
			.HasMaxLength(256);
	}
}
