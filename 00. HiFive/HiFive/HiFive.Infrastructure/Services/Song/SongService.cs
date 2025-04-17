using HiFive.Application.Contracts;
using HiFive.Application.DTOs.Song;
using HiFive.Application.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Services.Song;

public class SongService : ISongService
{
	private readonly IUnitOfWork _unitOfWork;

	public SongService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<SongDto> CreateSongAsync(string title, Guid artistId, Guid? albumId, uint duration,
		List<Guid> genreIds, DateTime releaseDate, string data)
	{
		if (string.IsNullOrWhiteSpace(title))
			throw new ArgumentException("Song title cannot be empty.", nameof(title));

		if (data == null || data.Length == 0)
			throw new ArgumentNullException(nameof(data), "Song data cannot be null or empty.");

		var artist = await _unitOfWork.Artists.GetByIdAsync(artistId);
		Validator.Validate(artist);

		var genres = await _unitOfWork.Genres.GetAllNoTracking()
			.Where(g => genreIds.Contains(g.Id))
			.ToListAsync();

		var song = new Domain.Models.Music.Song()
		{
			Title = title,
			ArtistId = artistId,
			AlbumId = albumId,
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

	public async Task<IEnumerable<SongDto>> GetSongsByPartialNameAsync(string partialName)
	{
		if (string.IsNullOrWhiteSpace(partialName))
			throw new ArgumentException("Partial name cannot be empty.", nameof(partialName));
		
		var songs = await _unitOfWork.Songs.GetAll()
			.Where(s => s.Title.Contains(partialName, StringComparison.CurrentCultureIgnoreCase))
			.ToListAsync();

		return songs.Select(SongDto.FromEntity);
	}

	public async Task<IEnumerable<SongDto>> GetAllSongsByGenreAsync(Guid genreId)
	{
		var genre = await _unitOfWork.Genres.GetByIdAsync(genreId);
		Validator.Validate(genre);

		var songs = await _unitOfWork.Songs.GetAll()
			.Where(s => s.Genres.Any(g => g.Id == genreId))
			.ToListAsync();

		return songs.Select(SongDto.FromEntity);
	}

	public async Task<SongDetailsDto> GetSongDetailsByIdAsync(Guid songId)
	{
		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		Validator.Validate(song);

		return SongDetailsDto.FromEntity(song);
	}

	public async Task<IEnumerable<SongDto>> GetSongsByArtistIdAsync(Guid artistId)
	{
		var artist = await _unitOfWork.Artists.GetByIdAsync(artistId);
		Validator.Validate(artist);

		var songs = await _unitOfWork.Songs.GetAllNoTracking()
			.Where(s => s.ArtistId == artistId)
			.ToListAsync();

		return songs.Select(SongDto.FromEntity);
	}

	public async Task UpdateSongAsync(Guid songId, string title, DateTime releaseDate)
	{
		if (string.IsNullOrWhiteSpace(title))
			throw new ArgumentException("Song title cannot be empty.", nameof(title));

		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		Validator.Validate(song);

		song.Title = title;
		song.ReleaseDate = releaseDate;

		await _unitOfWork.BeginTransactionAsync();
		_unitOfWork.Songs.Update(song);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task<IEnumerable<SongDto>> GetListenerLikedSongs(Guid listenerId)
	{
		var listener = await _unitOfWork.Listeners.GetByIdAsync(listenerId);
		Validator.Validate(listener);

		var songs = await _unitOfWork.Songs.GetAllNoTracking()
			.Where(s => s.LikedBy.Any(l => l.Id == listenerId))
			.ToListAsync();

		return songs.Select(SongDto.FromEntity);
	}
}
