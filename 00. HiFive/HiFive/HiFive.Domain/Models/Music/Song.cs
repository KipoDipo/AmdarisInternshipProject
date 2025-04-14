using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Users;

namespace HiFive.Domain.Models.Music;

public class Song : IDeletable
{
	public Guid Id { get; set; }

	public required string Title { get; set; }
	public TimeSpan Duration { get; set; }
	public DateTime ReleaseDate { get; set; }
	public ICollection<Genre> Genres { get; set; } = null!;
	public byte[] Data { get; set; } = null!;

	public Guid ArtistId { get; set; }
	public Artist Artist { get; set; } = null!;

	public Guid? AlbumId { get; set; }
	public Album? Album { get; set; }

	public bool IsDeleted { get; set; }
	public DateTime? DeletedOn { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
