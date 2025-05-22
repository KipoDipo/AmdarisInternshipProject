using HiFive.Domain.Models.Join;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiFive.Infrastructure.Db.Configurations;

public class PlaylistSongConfiguration : IEntityTypeConfiguration<PlaylistSong>
{
	public void Configure(EntityTypeBuilder<PlaylistSong> builder)
	{
		builder
			.HasOne(x => x.Playlist)
			.WithMany(p => p.Songs)
			.HasForeignKey(x => x.PlaylistId);

		builder
			.HasOne(x => x.Song)
			.WithMany(s => s.PlaylistsIn)
			.HasForeignKey(x => x.SongId);
	}
}
