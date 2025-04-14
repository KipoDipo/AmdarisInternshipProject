using HiFive.Application.DTOs.Genre;

namespace HiFive.Application.Services.Contracts;

public interface IGenreService
{
	Task<GenreDto> CreateGenreAsync(string name);
	Task<GenreDto> GetGenreByIdAsync(Guid genreId);
	Task<IEnumerable<GenreDto>> GetAllGenresBySongIdAsync(Guid songId);
	Task<IEnumerable<GenreDto>> GetAllGenresAsync();
	Task<IEnumerable<GenreDto>> GetAllGenresByPartialNameAsync(string partialName);
	Task UpdateGenreAsync(Guid genreId, string name);
	Task DeleteGenreAsync(Guid genreId);
}
