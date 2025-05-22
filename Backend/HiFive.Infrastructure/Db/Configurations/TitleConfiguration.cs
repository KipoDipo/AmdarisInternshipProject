using HiFive.Domain.Models.Throphies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiFive.Infrastructure.Db.Configurations;
public class TitleConfiguration : IEntityTypeConfiguration<Title>
{
	public void Configure(EntityTypeBuilder<Title> builder)
	{
		builder
			.Property(t => t.Name)
			.HasMaxLength(32);

		builder
			.Property(t => t.Description)
			.HasMaxLength(256);
	}
}