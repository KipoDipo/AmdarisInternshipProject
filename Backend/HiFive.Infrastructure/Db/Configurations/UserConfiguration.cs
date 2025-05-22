using HiFive.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiFive.Infrastructure.Db.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.UseTpcMappingStrategy();

		builder.Property(b => b.FirstName)
			.HasColumnType("nvarchar(32)");

		builder.Property(b => b.LastName)
			.HasColumnType("nvarchar(32)");

		builder.Property(b => b.DisplayName)
			.HasColumnType("nvarchar(32)");

		builder.Property(b => b.Bio)
			.HasColumnType("nvarchar(256)");
	}
}
