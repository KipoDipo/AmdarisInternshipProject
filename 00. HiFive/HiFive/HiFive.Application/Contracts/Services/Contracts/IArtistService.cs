using HiFive.Application.DTOs.Artist;

namespace HiFive.Application.Contracts.Services.Contracts;

public interface IArtistService
{
	Task<ArtistDto> CreateArtistAsync(ArtistCreateDto artistCreateDto);

	Task<IEnumerable<ArtistDto>> GetAllArtistsAsync();
	Task<ArtistDto> GetArtistByIdAsync(Guid artistId);
	Task<IEnumerable<ArtistDto>> GetArtistsByPartialNameAsync(string partialName);
	Task<ArtistDetailsDto> GetArtistDetailsByIdAsync(Guid artistId);

	Task UpdateArtistAsync(ArtistUpdateDto artistUpdateDto);

	Task DeleteArtistAsync(Guid artistId);
}
