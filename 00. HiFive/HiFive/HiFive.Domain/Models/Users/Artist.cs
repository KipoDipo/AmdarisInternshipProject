using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Music;

namespace HiFive.Domain.Models.Users;

public class Artist : ApplicationUser
{
	public ICollection<Listener> Followers { get; set; }
	public ICollection<Album> Albums { get; set; }
	public ICollection<Song> Singles { get; set; }

	public Distributor? Distributor { get; set; }
}
