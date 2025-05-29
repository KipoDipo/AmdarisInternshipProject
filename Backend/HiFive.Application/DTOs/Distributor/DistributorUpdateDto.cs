namespace HiFive.Application.DTOs.Distributor;

public class DistributorUpdateDto
{
	public Guid Id { get; set; }

	public string? DisplayName { get; set; }
	public string? Bio { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public Guid? ProfilePictureId { get; set; }
}