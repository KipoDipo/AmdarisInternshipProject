using HiFive.Application.DTOs.Song;

namespace HiFive.Presentation.Controllers.Requests.Music;

public class SongCreateRequest
{
	public required string Title { get; set; }
	public uint Duration { get; set; }
	public DateTime ReleaseDate { get; set; }
	public List<Guid> GenreIds { get; set; } = null!;
	public required IFormFile CoverImage { get; set; }

	public IFormFile Data { get; set; } = null!;

	public required Guid ArtistId { get; set; }
	public Guid? AlbumId { get; set; }

	public SongCreateDto ToSongCreateDto(Guid? coverImageId, string songUri)
	{
		return new SongCreateDto
		{
			Title = Title,
			Duration = Duration,
			ReleaseDate = ReleaseDate,
			GenreIds = GenreIds,
			CoverImageId = coverImageId,
			Data = songUri,
			ArtistId = ArtistId,
			AlbumId = AlbumId
		};
	}
}
