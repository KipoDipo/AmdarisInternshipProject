using HiFive.Domain.Contracts;

namespace HiFive.Domain.Models.Users;

public class Distributor : User
{
	public List<Artist> Artists { get; set; } = [];
	public bool IsApproved { get; set; } = false;
}
