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

	public bool IsApproved { get; set; }

	public static DistributorDetailsDto FromEntity(Domain.Models.Users.Distributor distributor)
	{
		return new DistributorDetailsDto
		{
			Id = distributor.Id,
			DisplayName = distributor.DisplayName,
			Bio = distributor.Bio,
			FirstName = distributor.FirstName,
			LastName = distributor.LastName,
			ProfilePictureId = distributor.ProfilePictureId,
			ArtistIds = distributor.Artists.Select(x => x.Id).ToList(),
			IsApproved = distributor.IsApproved
		};
	}
}
