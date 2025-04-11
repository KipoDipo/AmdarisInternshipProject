using HiFive.Domain.Contracts;

namespace HiFive.Domain.Models.Music;

public class Song : IDeletable
{
	public Guid Id { get; set; }

	public required string Title { get; set; }
	public TimeSpan Duration { get; set; }
	public DateTime ReleaseDate { get; set; }
	public ICollection<Genre> Genres { get; set; } = null!;
	public byte[] Data { get; set; } = null!;

	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
