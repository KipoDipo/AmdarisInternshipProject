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

		builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

		var entityTypes = builder.Model.GetEntityTypes();

		var foreignKeys = entityTypes
			.SelectMany(x => x.GetForeignKeys())
			.Where(x => x.DeleteBehavior == DeleteBehavior.Cascade);

		foreach (var foreignKey in foreignKeys)
		{
			foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
		}
	}
}
