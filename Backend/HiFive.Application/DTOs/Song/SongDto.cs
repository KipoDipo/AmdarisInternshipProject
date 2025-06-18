namespace HiFive.Application.DTOs.Song;

public class SongDto
{
	public Guid Id { get; set; }

	public required string Title { get; set; }
	public required Guid ArtistId { get; set; }
	public required string ArtistName { get; set; }
	public required string Data { get; set; }
	public string? Album { get; set; }
	public Guid? AlbumId { get; set; }
	public uint Duration { get; set; }
	public Guid? CoverImageId { get; set; }
	public List<Guid> GenreIds { get; set; } = [];

	public static SongDto FromEntity(Domain.Models.Music.Song song)
	{
		return new SongDto
		{
			Id = song.Id,
			Title = song.Title,
			ArtistId = song.ArtistId,
			Data = song.Data,
			ArtistName = song.ArtistName,
			Duration = song.Duration,
			Album = song.AlbumName,
			AlbumId = song.AlbumId,
			CoverImageId = song.CoverImageId,
			GenreIds = song.Genres.Select(x => x.Id).ToList()
		};
	}
}
