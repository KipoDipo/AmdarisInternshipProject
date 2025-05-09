using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Album;
using HiFive.Application.Exceptions;
using HiFive.Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Application.Services;

public class AlbumService : IAlbumService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IValidator _validator;

	public AlbumService(IUnitOfWork unitOfWork, IValidator validator)
	{
		_unitOfWork = unitOfWork;
		_validator = validator;
	}

	public async Task<AlbumDto> CreateAlbumAsync(AlbumCreateDto albumCreateDto)
	{
		if (string.IsNullOrWhiteSpace(albumCreateDto.Title))
			throw new UserInputException("Title cannot be empty.");

		var artist = await _unitOfWork.Artists.GetByIdAsync(albumCreateDto.ArtistId);
		_validator.Validate(artist);

		await _unitOfWork.BeginTransactionAsync();
		var album = new Domain.Models.Music.Album
		{
			Title = albumCreateDto.Title,
			Description = albumCreateDto.Description,
			ReleaseDate = albumCreateDto.ReleaseDate,
			ArtistId = albumCreateDto.ArtistId,
		};
		await _unitOfWork.Albums.AddAsync(album);

		foreach (var songId in albumCreateDto.SongIds)
		{
			var song = await _unitOfWork.Songs.GetByIdAsync(songId);
			_validator.Validate(song);

			song.Album = album;
		}

		await _unitOfWork.CommitTransactionAsync();
		return AlbumDto.FromEntity(album);
	}

	public async Task<AlbumDto> GetAlbumByIdAsync(Guid albumId)
	{
		var album = await _unitOfWork.Albums.GetByIdAsync(albumId);
		_validator.Validate(album);

		return AlbumDto.FromEntity(album);
	}

	public async Task<AlbumDetailsDto> GetAlbumDetailsByIdAsync(Guid albumId)
	{
		var album = await _unitOfWork.Albums.GetWithDetailsByIdAsync(albumId);
		_validator.Validate(album);

		return AlbumDetailsDto.FromEntity(album);
	}

	public async Task<IEnumerable<AlbumDto>> GetAllAlbumsByArtistAsync(Guid artistId)
	{
		var artist = await _unitOfWork.Artists.GetByIdAsync(artistId);
		_validator.Validate(artist);

		var albums = await _unitOfWork.Albums.GetAllNoTracking()
			.Where(a => a.ArtistId == artistId)
			.ToListAsync();

		return albums.Select(AlbumDto.FromEntity);
	}

	public async Task<IEnumerable<AlbumDto>> GetAllAlbumsByPartialTitleAsync(string partialTitle)
	{
		if (string.IsNullOrWhiteSpace(partialTitle))
			throw new UserInputException("Partial title cannot be empty.");

		var albums = await _unitOfWork.Albums.GetAllNoTracking()
			.Where(a => a.Title.Contains(partialTitle))
			.ToListAsync();

		return albums.Select(AlbumDto.FromEntity);
	}

	public async Task AddSongToAlbumAsync(Guid albumId, Guid songId)
	{
		var album = await _unitOfWork.Albums.GetByIdAsync(albumId);
		_validator.Validate(album);
		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		_validator.Validate(song);

		await _unitOfWork.BeginTransactionAsync();
		if (album.Songs.Any(s => s.Id == songId))
			throw new BadOperationException("Song already exists in the album.");

		album.Songs.Add(song);
		await _unitOfWork.Albums.UpdateAsync(album);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task UpdateAlbumAsync(AlbumUpdateDto albumUpdateDto)
	{
		if (string.IsNullOrWhiteSpace(albumUpdateDto.Title))
			throw new UserInputException("Album title cannot be empty.");

		var album = await _unitOfWork.Albums.GetByIdAsync(albumUpdateDto.Id);
		_validator.Validate(album);

		await _unitOfWork.BeginTransactionAsync();
		album.Title = albumUpdateDto.Title ?? album.Title;
		album.Description = albumUpdateDto.Description ?? album.Description;

		await _unitOfWork.Albums.UpdateAsync(album);
		await _unitOfWork.CommitTransactionAsync();
	}


	public async Task DeleteAlbumAsync(Guid albumId)
	{
		var album = await _unitOfWork.Albums.GetByIdAsync(albumId);
		_validator.Validate(album);

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Albums.DeleteAsync(albumId);
		await _unitOfWork.CommitTransactionAsync();
	}
}
