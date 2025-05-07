namespace HiFive.Application.DTOs.Song;

public class SongDetailsDto
{
	public Guid Id { get; set; }
	public string Title { get; set; } = null!;
	public uint Duration { get; set; }
	public DateTime? ReleaseDate { get; set; }
	public List<Guid> GenreIds { get; set; } = null!;
	public Guid ArtistId { get; set; }
	public Guid? CoverImageId { get; set; }

	public static SongDetailsDto FromEntity(Domain.Models.Music.Song song)
	{
		return new SongDetailsDto
		{
			Id = song.Id,
			Title = song.Title,
			Duration = song.Duration,
			ReleaseDate = song.ReleaseDate,
			GenreIds = song.Genres.Select(g => g.Id).ToList(),
			ArtistId = song.ArtistId,
			CoverImageId = song.CoverImageId
		};
	}
}
