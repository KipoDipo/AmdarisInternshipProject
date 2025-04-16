namespace HiFive.Application.DTOs.Artist;

public class ArtistDetailsDto
{
	public Guid Id { get; set; }

	public required string DisplayName { get; set; }

	public byte[]? ProfilePicture { get; set; }
	public string? Bio { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }

	public List<Guid> AlbumIds { get; set; } = [];
	public List<Guid> SingleIds { get; set; } = [];

	public static ArtistDetailsDto FromEntity(Domain.Models.Users.Artist artist)
	{
		return new ArtistDetailsDto
		{
			Id = artist.Id,
			DisplayName = artist.DisplayName,
			Bio = artist.Bio,
			FirstName = artist.FirstName,
			LastName = artist.LastName,
			ProfilePicture = artist.ProfilePicture,
			AlbumIds = artist.Albums.Select(a => a.Id).ToList(),
			SingleIds = artist.Singles.Select(s => s.Id).ToList()
		};
	}
}
