namespace HiFive.Application.DTOs.Album;

public class AlbumDto
{
	public Guid Id { get; set; }

	public required string Title { get; set; }
	public required DateTime ReleaseDate { get; set; }

	public Guid ArtistId { get; set; }
	public Guid? CoverImageId { get; set; }

	public static AlbumDto FromEntity(Domain.Models.Music.Album album)
	{
		return new AlbumDto
		{
			Id = album.Id,
			Title = album.Title,
			ReleaseDate = album.ReleaseDate,
			ArtistId = album.ArtistId,
			CoverImageId = album.CoverImageId
		};
	}
}
