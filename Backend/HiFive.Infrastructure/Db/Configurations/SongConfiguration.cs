using HiFive.Domain.Models.Music;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiFive.Infrastructure.Db.Configurations;
public class SongConfiguration : IEntityTypeConfiguration<Song>
{
	public void Configure(EntityTypeBuilder<Song> builder)
	{
		builder
			.Property(s => s.Title)
			.HasMaxLength(64);

		builder
			.Property(s => s.ArtistName)
			.HasMaxLength(64);

		builder
			.Property(s => s.AlbumName)
			.HasMaxLength(64);

		builder
			.HasOne(s => s.Artist)
			.WithMany(a => a.Singles)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasQueryFilter(s => !s.IsDeleted);
	}
}
