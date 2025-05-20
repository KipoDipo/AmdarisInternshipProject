using HiFive.Domain.Models.Join;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiFive.Infrastructure.Db.Configurations;

public class ListenerLikedSongConfiguration : IEntityTypeConfiguration<ListenerLikedSong>
{
	public void Configure(EntityTypeBuilder<ListenerLikedSong> builder)
	{
		builder
			.HasOne(l => l.Listener)
			.WithMany(l => l.LikedSongs)
			.HasForeignKey(l => l.ListenerId);

		builder
			.HasOne(l => l.LikedSong)
			.WithMany(s => s.LikedBy)
			.HasForeignKey(l => l.LikedSongId);
	}
}
