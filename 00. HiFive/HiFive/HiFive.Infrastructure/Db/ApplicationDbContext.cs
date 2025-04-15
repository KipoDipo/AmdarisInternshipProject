using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Users;
using HiFive.Domain.Models.Throphies;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HiFive.Infrastructure.Db.Configurations;

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

	//public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	//	: base(options)
	//{
	//}

	public ApplicationDbContext()
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=testing_grounds;Integrated Security=True;Encrypt=False");
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.ApplyConfiguration(new ApplicationUserConfiguration());

		builder.Entity<Playlist>()
			.HasOne(p => p.Owner)
			.WithMany(p => p.CreatedPlaylists);

		builder.Entity<Listener>()
			.HasMany(l => l.Badges)
			.WithMany(b => b.Owners);

		builder.Entity<Listener>()
			.HasOne(l => l.EquippedBadge);

		builder.Entity<Listener>()
			.HasMany(l => l.Titles)
			.WithMany(t => t.Owners);

		builder.Entity<Listener>()
			.HasOne(l => l.EquippedTitle);

		builder.Entity<ListenerFollower>()
			.HasKey(lf => new { lf.FollowerId, lf.FollowedId });

		builder.Entity<ListenerFollower>() // Listener that follows
			.HasOne(lf => lf.Follower)
			.WithMany(l => l.FollowingListeners)
			.OnDelete(DeleteBehavior.Restrict);

		builder.Entity<ListenerFollower>() // Listener that is being followed
			.HasOne(lf => lf.Followed)
			.WithMany(l => l.FollowedByListeners)
			.OnDelete(DeleteBehavior.Restrict);
	}

}
