namespace HiFive.Application.DTOs.Song;

public class SongUpdateDto
{
	public Guid Id { get; set; }

	public string? Title { get; set; }

	public string? Data { get; set; }
	public uint? Duration { get; set; }

	public Guid? AlbumId { get; set; }

	public Guid? CoverImageId { get; set; }
}