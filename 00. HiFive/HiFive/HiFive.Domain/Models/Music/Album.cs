using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Join;
using HiFive.Domain.Models.Misc;
using HiFive.Domain.Models.Users;

namespace HiFive.Domain.Models.Music;

public class Album : IDeletable
{
	public Guid Id { get; set; }

	public required string Title { get; set; }
	public string? Description { get; set; }
	public required DateTime ReleaseDate { get; set; }
	public List<AlbumSong> Songs { get; set; } = [];

	public Guid? CoverImageId { get; set; }
	public ImageFile? CoverImage { get; set; }

	public Guid ArtistId { get; set; }
	public Artist Artist { get; set; } = null!;

	public bool IsDeleted { get; set; }
	public DateTime? DeletedOn { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
