namespace HiFive.Application.DTOs.Song;

public class SongDto
{
	public Guid Id { get; set; }

	public required string Title { get; set; }
	public required Guid ArtistId { get; set; }
	public Guid? AlbumId { get; set; }
	public uint Duration { get; set; }

	public static SongDto FromEntity(Domain.Models.Music.Song song)
	{
		return new SongDto
		{
			Id = song.Id,
			Title = song.Title,
			ArtistId = song.ArtistId,
			Duration = song.Duration,
		};
	}
}
