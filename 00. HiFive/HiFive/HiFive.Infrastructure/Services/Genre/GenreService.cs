using HiFive.Application.Contracts;
using HiFive.Application.DTOs.Genre;
using HiFive.Application.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Services.Genre;

public class GenreService : IGenreService
{
	private readonly IUnitOfWork _unitOfWork;

	public GenreService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<GenreDto> CreateGenreAsync(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Genre name cannot be empty.", nameof(name));

		var genre = new Domain.Models.Music.Genre() 
		{ 
			Name = name 
		};

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Genres.AddAsync(genre);
		await _unitOfWork.CommitTransactionAsync();

		return GenreDto.FromEntity(genre);
	}

	public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
	{
		var genres = await _unitOfWork.Genres.GetAllNoTracking()
			.ToListAsync();

		return genres.Select(GenreDto.FromEntity);
	}


	public async Task<IEnumerable<GenreDto>> GetAllGenresBySongIdAsync(Guid songId)
	{
		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		Validator.Validate(song);

		var genres = await _unitOfWork.Genres.GetAllNoTracking()
			.Where(g => g.Songs.Any(s => s.Id == songId))
			.ToListAsync();

		return genres.Select(GenreDto.FromEntity);
	}

	public async Task<IEnumerable<GenreDto>> GetAllGenresByPartialNameAsync(string partialName)
	{
		if (string.IsNullOrWhiteSpace(partialName))
			throw new ArgumentException("Partial name cannot be empty.", nameof(partialName));

		var genres = await _unitOfWork.Genres.GetAllNoTracking()
			.Where(g => g.Name.Contains(partialName, StringComparison.CurrentCultureIgnoreCase))
			.ToListAsync();

		return genres.Select(GenreDto.FromEntity);
	}

	public async Task<GenreDto> GetGenreByIdAsync(Guid genreId)
	{
		var genre = await _unitOfWork.Genres.GetByIdAsync(genreId);
		Validator.Validate(genre);

		return GenreDto.FromEntity(genre);
	}

	public async Task UpdateGenreAsync(Guid genreId, string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Genre name cannot be empty.", nameof(name));

		var genre = await _unitOfWork.Genres.GetByIdAsync(genreId);
		Validator.Validate(genre);

		genre.Name = name;
		await _unitOfWork.BeginTransactionAsync();
		_unitOfWork.Genres.Update(genre);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task DeleteGenreAsync(Guid genreId)
	{
		var genre = await _unitOfWork.Genres.GetByIdAsync(genreId);
		Validator.Validate(genre);

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Genres.DeleteAsync(genreId);
		await _unitOfWork.CommitTransactionAsync();
	}
}
