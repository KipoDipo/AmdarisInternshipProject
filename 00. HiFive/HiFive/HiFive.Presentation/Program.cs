using HiFive.Application.Contracts;
using HiFive.Application.Services.Contracts;
using HiFive.Domain.Contracts;
using HiFive.Domain.Models.Users;
using HiFive.Infrastructure;
using HiFive.Infrastructure.Db;
using HiFive.Infrastructure.Services.Album;
using HiFive.Infrastructure.Services.Artist;
using HiFive.Infrastructure.Services.Genre;
using HiFive.Infrastructure.Services.Listener;
using HiFive.Infrastructure.Services.Playlist;
using HiFive.Infrastructure.Services.Song;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer("Data Source=localhost;Initial Catalog=testing_grounds;Integrated Security=True;Encrypt=False",
		b => b.MigrationsAssembly("HiFive.Infrastructure"));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IListenerService, ListenerService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddScoped<BaseUserManager<Listener>>();
builder.Services.AddScoped<BaseUserManager<Artist>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


