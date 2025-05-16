using HiFive.Domain.Models.Join;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiFive.Infrastructure.Db.Configurations;

public class AlbumSongConfiguration : IEntityTypeConfiguration<AlbumSong>
{
	public void Configure(EntityTypeBuilder<AlbumSong> builder)
	{
		builder
			.HasOne(x => x.Album)
			.WithMany(p => p.Songs)
			.HasForeignKey(x => x.AlbumId);

		builder
			.HasOne(x => x.Song)
			.WithOne(s => s.AlbumSong);
	}
}
