namespace HiFive.Application.DTOs.Song;

public class SongCreateDto
{
	public required string Title { get; set; }
	public uint? Duration { get; set; }
	public DateTime? ReleaseDate { get; set; }
	public required List<Guid> GenreIds { get; set; }
	public Guid? CoverImageId { get; set; }

	public required string Data { get; set; }

	public required Guid ArtistId { get; set; }
	public Guid? AlbumId { get; set; }
	public int? OrderIndex { get; set; }
}
