using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Users;

namespace HiFive.Domain.Models.Statistics;
public class ListenerData : IDeletable
{
	public Guid Id { get; set; }
	
	public Guid ListenerId { get; set; }
	public Listener Listener { get; set; } = null!;

	public Guid SongId { get; set; }
	public Song Song { get; set; } = null!;

	public DateTime ListenedOn { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedOn { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
