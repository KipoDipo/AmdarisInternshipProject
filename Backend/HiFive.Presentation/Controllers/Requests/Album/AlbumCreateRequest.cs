using HiFive.Application.DTOs.Album;
using HiFive.Presentation.Controllers.Requests.Music;

namespace HiFive.Presentation.Controllers.Requests.Album;

public class AlbumCreateRequest
{
	public required string Title { get; set; }
	public required Guid ArtistId { get; set; }
	public required string Description { get; set; }
	public required DateTime ReleaseDate { get; set; }
	public IFormFile? CoverImage { get; set; }
	public required List<SongCreateRequest> Songs { get; set; }

	public AlbumCreateDto ToAlbumCreateDto(Guid? coverImageId)
	{
		return new AlbumCreateDto
		{
			Title = Title,
			Description = Description,
			ReleaseDate = ReleaseDate,
			ArtistId = ArtistId,
			CoverImageId = coverImageId
		};
	}
}
