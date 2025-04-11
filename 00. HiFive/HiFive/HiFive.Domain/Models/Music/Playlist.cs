using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Users;

namespace HiFive.Domain.Models.Music;

public class Playlist : IDeletable
{
	public Guid Id { get; set; }

	public required string Title { get; set; }
	public string? Description { get; set; }
	public List<Song> Songs { get; set; } = [];

	public Guid OwnerId { get; set; }
	public Listener Owner { get; set; } = null!;

	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
