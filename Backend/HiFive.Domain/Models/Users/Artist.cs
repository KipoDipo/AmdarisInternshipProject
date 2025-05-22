using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Music;

namespace HiFive.Domain.Models.Users;

public class Artist : User
{
	public List<Listener> Followers { get; set; } = [];
	public List<Album> Albums { get; set; } = [];
	public List<Song> Singles { get; set; } = [];

	public Distributor? Distributor { get; set; }
}
