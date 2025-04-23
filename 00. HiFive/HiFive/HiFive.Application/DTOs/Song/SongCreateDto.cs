namespace HiFive.Application.DTOs.Song;

public class SongCreateDto
{
	public required string Title { get; set; }
	public uint Duration { get; set; }
	public DateTime ReleaseDate { get; set; }
	public List<Guid> GenreIds { get; set; } = null!;
	public byte[]? CoverImage { get; set; }

	public string Data { get; set; } = null!;

	public Guid ArtistId { get; set; }
	public Guid? AlbumId { get; set; }
}
