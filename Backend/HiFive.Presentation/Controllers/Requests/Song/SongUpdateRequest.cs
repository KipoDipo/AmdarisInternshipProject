using HiFive.Application.DTOs.Song;

namespace HiFive.Presentation.Controllers.Requests.Song;

public class SongUpdateRequest
{
	public Guid Id { get; set; }

	public string? Title { get; set; }
	public IFormFile? CoverImage { get; set; }

	public IFormFile? Data { get; set; } = null!;

	public Guid? AlbumId { get; set; }

	public SongUpdateDto SongUpdateDto(string songUri)
	{
		return new SongUpdateDto
		{
			Id = Id,
			Title = Title,
			Data = songUri,
			AlbumId = AlbumId
		};
	}
}
