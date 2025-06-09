using HiFive.Application.AwardSystem;
using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.Services;
using HiFive.Application.UnitOfWork;
using HiFive.Infrastructure;
using HiFive.Infrastructure.Db;
using HiFive.Infrastructure.Identity;
using HiFive.Presentation.Models;
using HiFive.Presentation.Policies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HiFive.Presentation.Extentions;

public static class ServiceCollectionExtentions
{
	public static IServiceCollection RegisterServices(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IValidator, Validator>();
		services.AddScoped<ISongService, SongService>();
		services.AddScoped<IAlbumService, AlbumService>();
		services.AddScoped<IPlaylistService, PlaylistService>();
		services.AddScoped<IGenreService, GenreService>();
		services.AddScoped<IArtistService, ArtistService>();
		services.AddScoped<IListenerService, ListenerService>();
		services.AddScoped<IPlaylistService, PlaylistService>();
		services.AddScoped<IAlbumService, AlbumService>();
		services.AddScoped<IImageFileService, ImageFileService>();
		services.AddScoped<ICurrentUserService, CurrentUserService>();
		services.AddScoped<ITrophyService, TrophyService>();
		services.AddScoped<IListenerDataService, ListenerDataService>();
		services.AddScoped<Awarder>();
		services.AddScoped<BlobService>();
		services.AddScoped<JwtService>();
		services.AddScoped<IAuthorizationHandler, VerifiedDistributorHandler>();
		services.AddScoped<IDistributorService, DistributorService>();


		return services;
	}

	public static IServiceCollection AddIdentityAndJwtAuthentication(this IServiceCollection services, IConfiguration config)
	{
		Jwt jwtOptions = config.GetSection(nameof(Jwt)).Get<Jwt>() ?? throw new InvalidOperationException("JWT not configured");

		services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

		services.Configure<IdentityOptions>(options =>
		{
			options.User.RequireUniqueEmail = true;
		});

		services.AddAuthorizationBuilder()
			.AddPolicy("ListenerOnly", policy => policy.RequireRole("Listener"))
			.AddPolicy("ArtistOnly", policy => policy.RequireRole("Artist"))
			.AddPolicy("DistributorOnly", policy => policy.RequireRole("Distributor"))
			.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
			.AddPolicy("VerifiedDistributorOnly", policy => policy.Requirements.Add(new VerifiedDistributorRequirement()));

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,

				ValidIssuer = jwtOptions.Issuer,
				ValidAudience = jwtOptions.Audience,
				IssuerSigningKey = new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(jwtOptions.SecretKey)
				)
			};
		});

		return services;
	}
}
