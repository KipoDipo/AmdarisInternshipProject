using HiFive.Domain.Models.Music;
using HiFive.Domain.Models;

namespace HiFive.Application.DTOs.Song;

public class SongDetailsDto
{
	public Guid Id { get; set; }
	public string Title { get; set; } = null!;
	public TimeSpan Duration { get; set; }
	public DateTime ReleaseDate { get; set; }
	public ICollection<Genre> Genres { get; set; } = null!;
	public Guid ArtistId { get; set; }

	public static SongDetailsDto FromEntity(Domain.Models.Music.Song song)
	{
		return new SongDetailsDto
		{
			Id = song.Id,
			Title = song.Title,
			Duration = song.Duration,
			ReleaseDate = song.ReleaseDate,
			Genres = song.Genres,
			ArtistId = song.ArtistId,
		};
	}
}
