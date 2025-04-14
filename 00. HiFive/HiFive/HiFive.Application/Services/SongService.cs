using HiFive.Application.Contracts;
using HiFive.Application.DTOs.Song;
using HiFive.Application.Services.Contracts;
using HiFive.Domain.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Application.Services;

public class SongService : ISongService
{
	private readonly IUnitOfWork _unitOfWork;

	public SongService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<SongDto> CreateSongAsync(string title, Guid artistId, TimeSpan duration,
		List<Genre> genres, DateTime releaseDate, byte[] data)
	{
		if (string.IsNullOrWhiteSpace(title))
			throw new ArgumentException("Song title cannot be empty.", nameof(title));

		if (duration <= TimeSpan.Zero)
			throw new ArgumentException("Song duration must be greater than zero.", nameof(duration));

		if (genres == null || genres.Count == 0)
			throw new ArgumentNullException(nameof(genres), "Genres cannot be null or empty.");

		if (data == null || data.Length == 0)
			throw new ArgumentNullException(nameof(data), "Song data cannot be null or empty.");

		var artist = await _unitOfWork.Artists.GetByIdNoTrackingAsync(artistId);

		Validator.Validate(artist);

		var song = new Song
		{
			Title = title,
			ArtistId = artistId,
			Duration = duration,
			Genres = genres,
			ReleaseDate = releaseDate,
			Data = data
		};

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Songs.AddAsync(song);
		await _unitOfWork.CommitTransactionAsync();

		return SongDto.FromEntity(song);
	}

	public async Task<SongDto> GetSongByIdAsync(Guid songId)
	{
		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		Validator.Validate(song);

		return SongDto.FromEntity(song);
	}

	public async Task<IEnumerable<SongDto>> GetSongsByPartialName(string partialName)
	{
		if (string.IsNullOrWhiteSpace(partialName))
			throw new ArgumentException("Partial name cannot be empty.", nameof(partialName));
		
		var songs = await _unitOfWork.Songs.GetAllAsync()
			.Where(s => s.Title.Contains(partialName, StringComparison.CurrentCultureIgnoreCase))
			.ToListAsync();

		return songs.Select(SongDto.FromEntity);
	}

	public async Task<SongDetailsDto> GetSongDetailsByIdAsync(Guid songId)
	{
		var song = await _unitOfWork.Songs.GetByIdNoTrackingAsync(songId);
		Validator.Validate(song);
		

		return SongDetailsDto.FromEntity(song);
	}

	public async Task<IEnumerable<SongDto>> GetSongsByArtistIdAsync(Guid artistId)
	{
		var artist = await _unitOfWork.Artists.GetByIdNoTrackingAsync(artistId);
		Validator.Validate(artist);

		var songs = await _unitOfWork.Songs.GetAllNoTrackingAsync()
			.Where(s => s.ArtistId == artistId)
			.ToListAsync();

		
		return songs.Select(SongDto.FromEntity);
	}

	public async Task<SongDto> UpdateSongAsync(Guid songId, string title, DateTime releaseDate)
	{
		if (string.IsNullOrWhiteSpace(title))
			throw new ArgumentException("Song title cannot be empty.", nameof(title));

		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		Validator.Validate(song);

		song.Title = title;
		song.ReleaseDate = releaseDate;

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Songs.UpdateAsync(song);
		await _unitOfWork.CommitTransactionAsync();
		
		return SongDto.FromEntity(song);
	}
}
