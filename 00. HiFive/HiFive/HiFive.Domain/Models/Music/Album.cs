using HiFive.Domain.Contracts;

namespace HiFive.Domain.Models.Music;

public class Album : IDeletable
{
	public Guid Id { get; set; }

	public required string Title { get; set; }
	public required DateTime ReleaseDate { get; set; }
	public List<Song> Songs { get; set; } = [];

	public Guid ArtistId { get; set; }
	public Artist Artist { get; set; } = null!;

	public bool IsDeleted { get; set; }
	public DateTime? DeletedOn { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
