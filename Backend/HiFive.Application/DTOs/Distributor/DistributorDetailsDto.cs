namespace HiFive.Application.DTOs.Distributor;

public class DistributorDetailsDto
{
	public Guid Id { get; set; }

	public required string DisplayName { get; set; }

	public Guid? ProfilePictureId { get; set; }
	public string? Bio { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }

	public List<Guid> ArtistIds { get; set; } = [];

	public static DistributorDetailsDto FromEntity(Domain.Models.Users.Distributor Distributor)
	{
		return new DistributorDetailsDto
		{
			Id = Distributor.Id,
			DisplayName = Distributor.DisplayName,
			Bio = Distributor.Bio,
			FirstName = Distributor.FirstName,
			LastName = Distributor.LastName,
			ProfilePictureId = Distributor.ProfilePictureId,
			ArtistIds = Distributor.Artists.Select(x => x.Id).ToList()
		};
	}
}
