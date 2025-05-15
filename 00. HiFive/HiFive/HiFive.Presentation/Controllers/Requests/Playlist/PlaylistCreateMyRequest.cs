using HiFive.Application.DTOs.Playlist;

namespace HiFive.Presentation.Controllers.Requests.Playlist;

public class PlaylistCreateMyRequest
{
	public required string Title { get; set; }

	public string? Description { get; set; }

	public IFormFile? Thumbnail { get; set; }

	public PlaylistCreateDto ToPlaylistCreateMyDto(Guid ownerId, Guid? thumbnailId)
	{
		return new PlaylistCreateDto()
		{
			Title = Title,
			Description = Description,
			ThumbnailId = thumbnailId,
			OwnerId = ownerId
		};
	}
}
