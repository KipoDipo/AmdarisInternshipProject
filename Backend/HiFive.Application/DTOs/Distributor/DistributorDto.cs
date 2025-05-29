namespace HiFive.Application.DTOs.Distributor;

public class DistributorDto
{
	public Guid Id { get; set; }

	public required string DisplayName { get; set; }

	public Guid? ProfilePictureId { get; set; }

	public static DistributorDto FromEntity(Domain.Models.Users.Distributor distributor)
	{
		return new DistributorDto
		{
			Id = distributor.Id,
			DisplayName = distributor.DisplayName,
			ProfilePictureId = distributor.ProfilePictureId
		};
	}
}
