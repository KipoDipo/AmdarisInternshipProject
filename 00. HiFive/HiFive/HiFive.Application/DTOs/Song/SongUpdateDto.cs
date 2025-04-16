namespace HiFive.Application.DTOs.Song;

public class SongUpdateDto
{
	public Guid Id { get; set; }

	public required string Title { get; set; }
	public DateTime ReleaseDate { get; set; }
}