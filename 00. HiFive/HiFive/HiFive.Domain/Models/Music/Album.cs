using HiFive.Domain.Contracts;

namespace HiFive.Domain.Models.Music;

public class Album : IDeletable
{
	public Guid Id { get; set; }

	public required string Title { get; set; }
	public required DateTime ReleaseDate { get; set; }
	public List<Song> Songs { get; set; } = [];

	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
