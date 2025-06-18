using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Song;
using HiFive.Application.Exceptions;
using HiFive.Application.UnitOfWork;
using HiFive.Domain.Models.Join;
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
			ArtistName = artist.DisplayName,
			Duration = (uint)songCreateDto.Duration!,
			CoverImageId = songCreateDto.CoverImageId,
			Genres = genres,
			ReleaseDate = songCreateDto.ReleaseDate,
			Data = songCreateDto.Data,
		};


		if (songCreateDto.AlbumId != null)
		{
			var album = await _unitOfWork.Albums.GetByIdAsync((Guid)songCreateDto.AlbumId);
			_validator.Validate(album);

			var albumSong = new AlbumSong()
			{
				AlbumId = (Guid)songCreateDto.AlbumId,
				SongId = song.Id,
				OrderIndex = (int)songCreateDto.OrderIndex!
			};
			song.AlbumId = songCreateDto.AlbumId;
			song.AlbumSong = albumSong;
			song.AlbumName = album.Title;
		}

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

	public async Task<IEnumerable<SongDto>> GetSongsByGenreAsync(Guid genreId, int pageNumber, int pageSize)
	{
		var genre = await _unitOfWork.Genres.GetByIdAsync(genreId);
		_validator.Validate(genre);

		var songs = await _unitOfWork.Songs.GetAllNoTracking()
			.Include(s => s.Genres)
			.Where(s => s.Genres.Any(g => g.Id == genreId))
			.Skip(pageSize * (pageNumber - 1))
			.Take(pageSize)
			.ToListAsync();

		return songs.Select(SongDto.FromEntity);
	}

	public async Task<IEnumerable<SongDto>> GetRandomSongsByGenre(Guid genreId, int count)
	{
		var genre = await _unitOfWork.Genres.GetByIdAsync(genreId);
		_validator.Validate(genre);

		var songs = await _unitOfWork.Songs.GetAllNoTracking()
			.Include(s => s.Genres)
			.Where(s => s.Genres.Any(g => g.Id == genreId))
			.OrderBy(_ => Guid.NewGuid())
			.Take(count)
			.ToListAsync();

		return songs.Select(SongDto.FromEntity);
	}

	public async Task<IEnumerable<SongDto>> GetSongsByArtistIdAsync(Guid artistId, int pageNumber, int pageSize)
	{
		var artist = await _unitOfWork.Artists.GetByIdAsync(artistId);
		_validator.Validate(artist);

		var songs = await _unitOfWork.Songs.GetAllNoTracking()
			.Include(s => s.Genres)
			.OrderByDescending(s => s.UpdatedOn)
			.Where(s => s.ArtistId == artistId)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
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

		song.Title = songUpdateDto.Title ?? song.Title;
		song.AlbumId = songUpdateDto.AlbumId ?? song.AlbumId;
		song.Data = songUpdateDto.Data ?? song.Data;
		song.CoverImageId = songUpdateDto.CoverImageId ?? song.CoverImageId;
		song.Duration = songUpdateDto.Duration ?? song.Duration;

		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task<IEnumerable<SongDto>> GetListenerLikedSongs(Guid listenerId)
	{
		var listener = await _unitOfWork.Listeners.GetAllNoTracking()
			.Include(l => l.LikedSongs)
				.ThenInclude(l => l.LikedSong)
			.Where(l => l.Id == listenerId)
			.FirstOrDefaultAsync();

		_validator.Validate(listener);

		return listener.LikedSongs.Select(l => SongDto.FromEntity(l.LikedSong));
	}

	public async Task<IEnumerable<SongDto>> GetAllSongsByPlaylistIdAsync(Guid playlistId)
	{
		var playlist = await _unitOfWork.Playlists.GetWithDetailsByIdAsync(playlistId);
		_validator.Validate(playlist);

		return playlist.Songs
			.OrderBy(s => s.OrderIndex)
			.Select(x => SongDto.FromEntity(x.Song));
	}

	public async Task<IEnumerable<SongDto>> GetAllSongsByAlbumIdAsync(Guid albumId)
	{
		var album = await _unitOfWork.Albums.GetWithDetailsByIdAsync(albumId); // TODO: there's 1 extra join here for Artist
		_validator.Validate(album);

		return album.Songs
			.OrderBy(s => s.OrderIndex)
			.Select(a => SongDto.FromEntity(a.Song));
	}

	public async Task DeleteSongById(Guid id)
	{
		var song = await _unitOfWork.Songs.GetByIdAsync(id);
		_validator.Validate(song);

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Songs.DeleteAsync(id);
		await _unitOfWork.CommitTransactionAsync();
	}

}
