namespace HiFive.Application.DTOs.Artist;

public class ArtistDto
{
	public Guid Id { get; set; }

	public required string DisplayName { get; set; }

	public byte[]? ProfilePicture { get; set; }

	public static ArtistDto FromEntity(Domain.Models.Users.Artist artist)
	{
		return new ArtistDto
		{
			Id = artist.Id,
			DisplayName = artist.DisplayName,
			ProfilePicture = artist.ProfilePicture
		};
	}
}
