namespace HiFive.Application.DTOs.Album;

public class AlbumDetailsDto
{
	public Guid Id { get; set; }

	public required string Title { get; set; }

	public string? Description { get; set; }

	public Guid? CoverImageId { get; set; }

	public required DateTime ReleaseDate { get; set; }

	public Guid ArtistId { get; set; }

	public List<Guid> SongIds { get; set; } = [];

	public static AlbumDetailsDto FromEntity(Domain.Models.Music.Album album)
	{
		return new AlbumDetailsDto
		{
			Id = album.Id,
			Title = album.Title,
			Description = album.Description,
			ReleaseDate = album.ReleaseDate,
			ArtistId = album.ArtistId,
			CoverImageId = album.CoverImageId,
			SongIds = album.Songs.Select(s => s.Id).ToList()
		};
	}
}

