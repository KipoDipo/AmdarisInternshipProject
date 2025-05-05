using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Misc;
using HiFive.Domain.Models.Users;

namespace HiFive.Domain.Models.Music;

public class Playlist : IDeletable
{
	public Guid Id { get; set; }

	public required string Title { get; set; }
	public string? Description { get; set; }
	public List<Song> Songs { get; set; } = [];


	public Guid? ThumbnailId { get; set; }
	public ImageFile? Thumbnail { get; set; }

	public Guid OwnerId { get; set; }
	public Listener Owner { get; set; } = null!;

	public List<Listener> LikedByListeners { get; set; } = [];

	public bool IsDeleted { get; set; }
	public DateTime? DeletedOn { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
