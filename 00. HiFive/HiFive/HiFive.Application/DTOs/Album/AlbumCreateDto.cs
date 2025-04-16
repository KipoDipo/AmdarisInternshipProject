namespace HiFive.Application.DTOs.Album;

public class AlbumCreateDto
{
	public required string Title { get; set; }

	public string? Description { get; set; }

	public required DateTime ReleaseDate { get; set; }

	public Guid ArtistId { get; set; }

	public List<Guid> SongIds { get; set; } = [];
}
