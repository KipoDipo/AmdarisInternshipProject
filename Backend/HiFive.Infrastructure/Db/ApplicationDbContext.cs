using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Join;
using HiFive.Domain.Models.Misc;
using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Throphies;
using HiFive.Domain.Models.Users;
using HiFive.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Db;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
	#region Users
	public DbSet<Listener> Listeners { get; set; }
	public DbSet<Artist> Artists { get; set; }
	public DbSet<Distributor> Distributors { get; set; }
	public DbSet<Admin> Admins { get; set; }
	#endregion

	#region Music
	public DbSet<Album> Albums { get; set; }
	public DbSet<Genre> Genres { get; set; }
	public DbSet<Song> Songs { get; set; }
	public DbSet<Playlist> Playlists { get; set; }
	#endregion

	#region Throphies
	public DbSet<Badge> Badges { get; set; }
	public DbSet<Title> Titles { get; set; }
	#endregion

	#region Misc
	public DbSet<ImageFile> ImageFiles { get; set; }
	#endregion

	#region Join
	public DbSet<PlaylistSong> PlaylistSongs { get; set; }
	public DbSet<ListenerLikedSong> ListenerLikedSongs { get; set; }
	public DbSet<AlbumSong> AlbumSongs { get; set; }
	#endregion

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
	}

	public override int SaveChanges()
	{
		ApplyAuditInfo();
		return base.SaveChanges();
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		ApplyAuditInfo();
		return await base.SaveChangesAsync(cancellationToken);
	}

	private void ApplyAuditInfo()
	{
		var entries = ChangeTracker.Entries<IDeletable>();

		foreach (var entry in entries)
		{
			switch (entry.State)
			{
				case EntityState.Added:
				{
					entry.Entity.CreatedOn = DateTime.Now;
					entry.Entity.UpdatedOn = DateTime.Now;

					break;
				}
				case EntityState.Modified:
				{
					entry.Entity.UpdatedOn = DateTime.Now;

					break;
				}
				case EntityState.Deleted:
				{
					entry.State = EntityState.Modified;

					entry.Entity.IsDeleted = true;
					entry.Entity.DeletedOn = DateTime.Now;
					break;
				}

				default:
					break;
			}
		}
	}
}
