using HiFive.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiFive.Infrastructure.Db.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		// ============================== Setting up column types ==============================
		builder.Property(b => b.FirstName)
			.HasColumnType("nvarchar(32)");

		builder.Property(b => b.LastName)
			.HasColumnType("nvarchar(32)");

		builder.Property(b => b.Email)
			.HasColumnType("varchar(128)");

		builder.Property(b => b.NormalizedEmail)
			.HasColumnType("varchar(128)");

		builder.Property(b => b.UserName)
			.HasColumnType("varchar(16)");

		builder.Property(b => b.NormalizedUserName)
			.HasColumnType("varchar(16)");

		builder.Property(b => b.Bio)
			.HasColumnType("nvarchar(256)");

		// ============================== Setting up indexes ==============================
		builder.HasIndex(b => b.Email)
			.IsUnique();
		
		builder.HasIndex(b => b.NormalizedEmail)
			.IsUnique();
	}
}
