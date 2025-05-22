using HiFive.Application.Contracts.Repositories;
using HiFive.Application.DTOs.Artist;
using HiFive.Domain.Models.Misc;
using HiFive.Domain.Models.Users;
using HiFive.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
{
	private readonly UserManager<ApplicationUser> _userManager;

	public ArtistRepository(DbContext dbContext, UserManager<ApplicationUser> userManager) : base(dbContext)
	{
		_userManager = userManager;
	}

	public async Task<IEnumerable<Artist>> GetAllByPartialName(string partialName)
	{
		return await _dbContext.Set<Artist>()
			.Where(a => a.DisplayName.Contains(partialName))
			.ToListAsync();
	}

	public override async Task<Artist?> GetWithDetailsByIdAsync(Guid id)
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

		await _userManager.CreateAsync(newArtist, artistCreateDto.Password);

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
