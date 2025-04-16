namespace HiFive.Application.DTOs.Artist;

public class ArtistUpdateDto
{
	public Guid Id { get; set; }

	public string? DisplayName { get; set; }
	public string? Bio { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public byte[]? ProfilePicture { get; set; }
}