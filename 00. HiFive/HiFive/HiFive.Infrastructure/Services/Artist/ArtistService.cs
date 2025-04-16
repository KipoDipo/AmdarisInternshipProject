using HiFive.Application.DTOs.Artist;
using HiFive.Application.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Services.Artist;

public class ArtistService : IArtistService
{
	private readonly BaseUserManager<Domain.Models.Users.Artist> _userManager;

	public ArtistService(BaseUserManager<Domain.Models.Users.Artist> userManager)
	{
		_userManager = userManager;
	}

	public async Task<ArtistDto> CreateArtistAsync(ArtistCreateDto artistCreateDto)
	{
		if (string.IsNullOrWhiteSpace(artistCreateDto.DisplayName))
			throw new ArgumentException("Artist display name cannot be empty.", nameof(artistCreateDto.DisplayName));

		var artist = new Domain.Models.Users.Artist()
		{
			UserName = artistCreateDto.UserName,
			DisplayName = artistCreateDto.DisplayName,
			FirstName = artistCreateDto.FirstName,
			LastName = artistCreateDto.LastName,
			Email = artistCreateDto.Email,
			Bio = artistCreateDto.Bio,
			ProfilePicture = artistCreateDto.ProfilePicture,
		};
		await _userManager.CreateAsync(artist, artistCreateDto.Password);
		//await _userManager.AddToRoleAsync(artist, "Artist");
		return ArtistDto.FromEntity(artist);
	}

	public async Task DeleteArtistAsync(Guid artistId)
	{
		throw new NotImplementedException();
	}

	public async Task<ArtistDto> GetArtistByIdAsync(Guid artistId)
	{
		//var artist = await _userManager.FindByIdAsync(artistId.ToString());
		var artist = await _userManager.Users
			.FirstOrDefaultAsync(a => a.Id == artistId);
		Validator.Validate(artist);
		

		return ArtistDto.FromEntity(artist);
	}

	public async Task<ArtistDetailsDto> GetArtistDetailsByIdAsync(Guid artistId)
	{
		var artist = await _userManager.Users
			.Include(a => a.Albums)
			.Include(a => a.Singles)
			.FirstOrDefaultAsync(a => a.Id == artistId);
		Validator.Validate(artist);

		return ArtistDetailsDto.FromEntity(artist);
	}

	public async Task<IEnumerable<ArtistDto>> GetArtistsByPartialNameAsync(string partialName)
	{
		if (string.IsNullOrWhiteSpace(partialName))
			throw new ArgumentException("Partial name cannot be empty.", nameof(partialName));
		var artists = await _userManager.Users
			.Where(a => a.DisplayName.Contains(partialName))
			.ToListAsync();

		return artists.Select(ArtistDto.FromEntity);
	}

	public async Task<ArtistDto> UpdateArtistAsync(ArtistUpdateDto artistUpdateDto)
	{
		var artist = await _userManager.Users
			.FirstOrDefaultAsync(a => a.Id == artistUpdateDto.Id);
		Validator.Validate(artist);

		artist.DisplayName = artistUpdateDto.DisplayName ?? artist.DisplayName;
		artist.FirstName = artistUpdateDto.FirstName ?? artist.FirstName;
		artist.LastName = artistUpdateDto.LastName ?? artist.LastName;
		artist.Bio = artistUpdateDto.Bio ?? artist.Bio;
		artist.ProfilePicture = artistUpdateDto.ProfilePicture ?? artist.ProfilePicture;

		await _userManager.UpdateAsync(artist);
		//await _userManager.UpdateSecurityStampAsync(artist); ??
		return ArtistDto.FromEntity(artist);
	}
}
