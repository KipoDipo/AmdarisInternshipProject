using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Song;
using HiFive.Application.Exceptions;
using HiFive.Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Application.Services;

public class SongService : ISongService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IValidator _validator;

	public SongService(IUnitOfWork unitOfWork, IValidator validator)
	{
		_unitOfWork = unitOfWork;
		_validator = validator;
	}

	public async Task<SongDto> CreateSongAsync(SongCreateDto songCreateDto)
	{
		if (string.IsNullOrWhiteSpace(songCreateDto.Title))
			throw new UserInputException("Song title cannot be empty.");

		if (songCreateDto.Data == null || songCreateDto.Data.Length == 0)
			throw new UserInputException("Song data cannot be null or empty.");

		var artist = await _unitOfWork.Artists.GetByIdAsync(songCreateDto.ArtistId);
		_validator.Validate(artist);

		var genres = await _unitOfWork.Genres.GetAll()
			.Where(g => songCreateDto.GenreIds.Contains(g.Id))
			.ToListAsync();

		var song = new Domain.Models.Music.Song()
		{
			Title = songCreateDto.Title,
			ArtistId = songCreateDto.ArtistId,
			AlbumId = songCreateDto.AlbumId,
			Duration = songCreateDto.Duration,
			CoverImageId = songCreateDto.CoverImageId,
			Genres = genres,
			ReleaseDate = songCreateDto.ReleaseDate,
			Data = songCreateDto.Data,
		};

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Songs.AddAsync(song);
		await _unitOfWork.CommitTransactionAsync();

		return SongDto.FromEntity(song);
	}

	public async Task<SongDto> GetSongByIdAsync(Guid songId)
	{
		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		_validator.Validate(song);

		return SongDto.FromEntity(song);
	}

	public async Task<IEnumerable<SongDto>> GetAllSongsAsync()
	{
		var songs = await _unitOfWork.Songs.GetAllNoTracking()
			.Include(s => s.Artist)
			.ToListAsync();

		return songs.Select(SongDto.FromEntity);
	}

	public async Task<SongDetailsDto> GetSongDetailsByIdAsync(Guid songId)
	{
		var song = await _unitOfWork.Songs.GetWithDetailsByIdAsync(songId);
		_validator.Validate(song);

		return SongDetailsDto.FromEntity(song);
	}
	public async Task<IEnumerable<SongDto>> GetSongsByPartialNameAsync(string partialName)
	{
		if (string.IsNullOrWhiteSpace(partialName))
			throw new UserInputException("Partial name cannot be empty.");

		var songs = await _unitOfWork.Songs.GetAllByPartialName(partialName);

		return songs.Select(SongDto.FromEntity);
	}

	public async Task<IEnumerable<SongDto>> GetAllSongsByGenreAsync(Guid genreId)
	{
		var genre = await _unitOfWork.Genres.GetByIdAsync(genreId);
		_validator.Validate(genre);

		var songs = await _unitOfWork.Songs.GetAll()
			.Where(s => s.Genres.Any(g => g.Id == genreId))
			.ToListAsync();

		return songs.Select(SongDto.FromEntity);
	}

	public async Task<IEnumerable<SongDto>> GetSongsByArtistIdAsync(Guid artistId)
	{
		var artist = await _unitOfWork.Artists.GetByIdAsync(artistId);
		_validator.Validate(artist);

		var songs = await _unitOfWork.Songs.GetAllNoTracking()
			.Include(s => s.Artist)
			.Include(s => s.AlbumSong)
			.Where(s => s.ArtistId == artistId)
			.ToListAsync();

		return songs.Select(SongDto.FromEntity);
	}

	public async Task UpdateSongAsync(SongUpdateDto songUpdateDto)
	{
		if (string.IsNullOrWhiteSpace(songUpdateDto.Title))
			throw new UserInputException("Song title cannot be empty.");

		var song = await _unitOfWork.Songs.GetByIdAsync(songUpdateDto.Id);
		_validator.Validate(song);

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Songs.UpdateAsync(song);

		song.Title = songUpdateDto.Title;
		song.ReleaseDate = songUpdateDto.ReleaseDate;

		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task<IEnumerable<SongDto>> GetListenerLikedSongs(Guid listenerId)
	{
		var listener = await _unitOfWork.Listeners.GetAllNoTracking()
			.Include(l => l.LikedSongs)
			.ThenInclude(s => s.Artist)
			.Where(l => l.Id == listenerId)
			.FirstOrDefaultAsync();

		_validator.Validate(listener);

		return listener.LikedSongs.Select(SongDto.FromEntity);
	}

	public async Task<IEnumerable<SongDto>> GetAllSongsByPlaylistIdAsync(Guid playlistId)
	{
		var playlist = await _unitOfWork.Playlists.GetWithDetailsByIdAsync(playlistId);
		_validator.Validate(playlist);

		return playlist.Songs.OrderBy(s => s.OrderIndex).Select(x => SongDto.FromEntity(x.Song));
	}

	public async Task<IEnumerable<SongDto>> GetAllSongsByAlbumIdAsync(Guid albumId)
	{
		var album = await _unitOfWork.Albums.GetWithDetailsByIdAsync(albumId); // TODO: there's 1 extra join here for Artist
		_validator.Validate(album);

		return album.Songs.OrderBy(s => s.OrderIndex).Select(a => SongDto.FromEntity(a.Song));
	}

}
