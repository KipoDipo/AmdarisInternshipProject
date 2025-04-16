namespace HiFive.Application.DTOs.Artist;

public class ArtistCreateDto
{
	public required string UserName { get; set; }
	public required string Email { get; set; }
	public required string Password { get; set; }
	public required string DisplayName { get; set; }

	public string? Bio { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public byte[]? ProfilePicture { get; set; }
}
