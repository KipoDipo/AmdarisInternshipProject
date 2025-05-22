using HiFive.Application.DTOs.Genre;

namespace HiFive.Application.Contracts.Services.Contracts;

public interface IGenreService
{
	Task<GenreDto> CreateGenreAsync(GenreCreateDto genreCreateDto);
	Task<GenreDto> GetGenreByIdAsync(Guid genreId);
	Task<IEnumerable<GenreDto>> GetAllGenresBySongIdAsync(Guid songId);
	Task<IEnumerable<GenreDto>> GetAllGenresAsync();
	Task<IEnumerable<GenreDto>> GetAllGenresByPartialNameAsync(string partialName);
	Task UpdateGenreAsync(GenreDto genreDto);
	Task DeleteGenreAsync(Guid genreId);
}
