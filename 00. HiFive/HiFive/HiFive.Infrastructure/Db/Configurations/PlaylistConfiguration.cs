using HiFive.Domain.Models.Music;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiFive.Infrastructure.Db.Configurations;
public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{

	public void Configure(EntityTypeBuilder<Playlist> builder)
	{
		builder
			.Property(p => p.Title)
			.HasMaxLength(64);

		builder
			.Property(p => p.Description)
			.HasMaxLength(256);

		builder
			.HasOne(p => p.Owner)
			.WithMany(p => p.CreatedPlaylists)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
