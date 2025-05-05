using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Artist;
using HiFive.Application.UnitOfWork;
using HiFive.Domain.Models.Misc;

namespace HiFive.Application.Services;

public class ArtistService : IArtistService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IValidator _validator;

	public ArtistService(IUnitOfWork unitOfWork, IValidator validator)
	{
		_unitOfWork = unitOfWork;
		_validator = validator;
	}

	public async Task<ArtistDto> CreateArtistAsync(ArtistCreateDto artistCreateDto)
	{
		if (string.IsNullOrWhiteSpace(artistCreateDto.DisplayName))
			throw new ArgumentException("Artist display name cannot be empty.", nameof(artistCreateDto.DisplayName));

		await _unitOfWork.BeginTransactionAsync();
		var artist = await _unitOfWork.Artists.Register(artistCreateDto);
		await _unitOfWork.CommitTransactionAsync();
		return ArtistDto.FromEntity(artist);
	}

	public async Task DeleteArtistAsync(Guid artistId)
	{
		var artist = await _unitOfWork.Artists.GetByIdAsync(artistId);
		_validator.Validate(artist);

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Artists.DeleteAsync(artistId);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task<ArtistDto> GetArtistByIdAsync(Guid artistId)
	{
		var artist = await _unitOfWork.Artists.GetByIdAsync(artistId);
		_validator.Validate(artist);

		return ArtistDto.FromEntity(artist);
	}

	public async Task<ArtistDetailsDto> GetArtistDetailsByIdAsync(Guid artistId)
	{
		var artist = await _unitOfWork.Artists.GetWithDetailsByIdAsync(artistId);
		_validator.Validate(artist);

		return ArtistDetailsDto.FromEntity(artist);
	}

	public async Task<IEnumerable<ArtistDto>> GetArtistsByPartialNameAsync(string partialName)
	{
		if (string.IsNullOrWhiteSpace(partialName))
			throw new ArgumentException("Partial name cannot be empty.", nameof(partialName));
		var artists = await _unitOfWork.Artists.GetAllByPartialName(partialName);

		return artists.Select(ArtistDto.FromEntity);
	}

	public async Task UpdateArtistAsync(ArtistUpdateDto artistUpdateDto)
	{
		var artist = await _unitOfWork.Artists.GetWithDetailsByIdAsync(artistUpdateDto.Id);
		_validator.Validate(artist);
		
		await _unitOfWork.BeginTransactionAsync();
		artist.DisplayName = artistUpdateDto.DisplayName ?? artist.DisplayName;
		artist.FirstName = artistUpdateDto.FirstName ?? artist.FirstName;
		artist.LastName = artistUpdateDto.LastName ?? artist.LastName;
		artist.Bio = artistUpdateDto.Bio ?? artist.Bio;

		await _unitOfWork.Artists.UpdateAsync(artist);
		await _unitOfWork.CommitTransactionAsync();
		//await _userManager.UpdateSecurityStampAsync(artist); ??
		//TODO update passowrd
	}
}
