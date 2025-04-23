using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Genre;
using HiFive.Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Application.Services;

public class GenreService : IGenreService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IValidator _validator;

	public GenreService(IUnitOfWork unitOfWork, IValidator validator)
	{
		_unitOfWork = unitOfWork;
		_validator = validator;
	}

	public async Task<GenreDto> CreateGenreAsync(GenreCreateDto genreCreateDto)
	{
		if (string.IsNullOrWhiteSpace(genreCreateDto.Name))
			throw new ArgumentException("Genre name cannot be empty.", nameof(genreCreateDto.Name));

		var genre = new Domain.Models.Music.Genre()
		{
			Name = genreCreateDto.Name
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
		_validator.Validate(song);

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
		_validator.Validate(genre);

		return GenreDto.FromEntity(genre);
	}

	public async Task UpdateGenreAsync(GenreDto genreDto)
	{
		if (string.IsNullOrWhiteSpace(genreDto.Name))
			throw new ArgumentException("Genre name cannot be empty.", nameof(genreDto.Name));

		var genre = await _unitOfWork.Genres.GetByIdAsync(genreDto.Id);
		_validator.Validate(genre);

		await _unitOfWork.BeginTransactionAsync();
		genre.Name = genreDto.Name;
		await _unitOfWork.Genres.UpdateAsync(genre);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task DeleteGenreAsync(Guid genreId)
	{
		var genre = await _unitOfWork.Genres.GetByIdAsync(genreId);
		_validator.Validate(genre);

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Genres.DeleteAsync(genreId);
		await _unitOfWork.CommitTransactionAsync();
	}
}
