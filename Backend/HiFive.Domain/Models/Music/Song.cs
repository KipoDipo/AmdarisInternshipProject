using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Join;
using HiFive.Domain.Models.Misc;
using HiFive.Domain.Models.Users;

namespace HiFive.Domain.Models.Music;

public class Song : IDeletable
{
	public Guid Id { get; set; }

	public required string Title { get; set; }
	public uint Duration { get; set; }
	public DateTime? ReleaseDate { get; set; }
	public List<Genre> Genres { get; set; } = [];
	public string Data { get; set; } = null!; // Azure Blob Storage

	public Guid? CoverImageId { get; set; }
	public ImageFile? CoverImage { get; set; }

	public Guid ArtistId { get; set; }
	public Artist Artist { get; set; } = null!;
	public string ArtistName { get; set; } = null!;

	public Guid? AlbumId { get; set; }
	public AlbumSong? AlbumSong { get; set; }
	public string AlbumName { get; set; } = null!;

	public List<PlaylistSong> PlaylistsIn { get; set; } = [];
	public List<ListenerLikedSong> LikedBy { get; set; } = [];

	public bool IsDeleted { get; set; }
	public DateTime? DeletedOn { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
