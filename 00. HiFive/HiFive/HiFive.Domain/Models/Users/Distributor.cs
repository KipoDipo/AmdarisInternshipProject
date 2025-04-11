using HiFive.Domain.Contracts;

namespace HiFive.Domain.Models.Users;

public class Distributor : ApplicationUser
{
	public List<Artist> Artists { get; set; } = [];
}
