using HiFive.Application.Contracts.Repositories;
using HiFive.Application.DTOs.Artist;
using HiFive.Domain.Models.Misc;
using HiFive.Domain.Models.Users;
using HiFive.Infrastructure.Exceptions;
using HiFive.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole<Guid>> _roleManager;

	public ArtistRepository(DbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager) : base(dbContext)
	{
		_userManager = userManager;
		_roleManager = roleManager;
	}

	public async Task<IEnumerable<Artist>> GetAllByPartialName(string partialName)
	{
		return await _dbContext.Set<Artist>()
			.Where(a => a.DisplayName.Contains(partialName))
			.ToListAsync();
	}

	public async Task<Artist?> GetWithDetailsByIdAsync(Guid id)
	{
		return await _dbContext.Set<Artist>()
			.Include(a => a.Albums)
			.Include(a => a.Singles)
			.FirstOrDefaultAsync(a => a.Id == id);
	}

	public async Task<Artist> Register(ArtistCreateDto artistCreateDto)
	{
		ApplicationUser newArtist = new()
		{
			UserName = artistCreateDto.UserName,
			Email = artistCreateDto.Email,
			PhoneNumber = artistCreateDto.PhoneNumber,
		};

		if (!await _roleManager.RoleExistsAsync("Artist"))
		{
			await _roleManager.CreateAsync(new IdentityRole<Guid>("Artist"));
		}

		var result = await _userManager.CreateAsync(newArtist, artistCreateDto.Password);

		if (!result.Succeeded)
			throw new IdentityCreationException(result.Errors);

		await _userManager.AddToRoleAsync(newArtist, "Artist");

		var artist = new Artist()
		{
			Id = newArtist.Id,
			DisplayName = artistCreateDto.DisplayName,
			FirstName = artistCreateDto.FirstName,
			LastName = artistCreateDto.LastName,
			Bio = artistCreateDto.Bio,
			ProfilePictureId = artistCreateDto.ProfilePictureId,
		};
		
		await _dbContext.AddAsync(artist);
		return artist;
	}
}
