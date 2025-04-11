using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Users;

namespace HiFive.Domain.Models;

public class Artist : ApplicationUser
{
	public ICollection<Listener> Followers { get; set; }
	public ICollection<Album> Albums { get; set; }
	public ICollection<Song> Singles { get; set; }

	public Distributor? Distributor { get; set; }
}
