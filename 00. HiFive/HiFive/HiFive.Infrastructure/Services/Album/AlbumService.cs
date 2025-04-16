using HiFive.Application.Contracts;
using HiFive.Application.DTOs.Album;
using HiFive.Application.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Services.Album;

public class AlbumService : IAlbumService
{
	private readonly IUnitOfWork _unitOfWork;

	public AlbumService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<AlbumDto> CreateAlbumAsync(AlbumCreateDto albumCreateDto)
	{
		if (string.IsNullOrWhiteSpace(albumCreateDto.Title))
			throw new ArgumentException("Title cannot be empty.", nameof(albumCreateDto.Title));

		var artist = await _unitOfWork.Artists.GetByIdAsync(albumCreateDto.ArtistId);
		Validator.Validate(artist);

		var album = new Domain.Models.Music.Album
		{
			Title = albumCreateDto.Title,
			Description = albumCreateDto.Description,
			ReleaseDate = albumCreateDto.ReleaseDate,
			ArtistId = albumCreateDto.ArtistId,
		};

		foreach (var songId in albumCreateDto.SongIds)
		{
			var song = await _unitOfWork.Songs.GetByIdAsync(songId);
			Validator.Validate(song);

			album.Songs.Add(song);
		}

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Albums.AddAsync(album);
		await _unitOfWork.CommitTransactionAsync();
		return AlbumDto.FromEntity(album);
	}

	public async Task<AlbumDto> GetAlbumByIdAsync(Guid albumId)
	{
		var album = await _unitOfWork.Albums.GetByIdAsync(albumId);
		Validator.Validate(album);

		return AlbumDto.FromEntity(album);
	}

	public async Task<AlbumDetailsDto> GetAlbumDetailsByIdAsync(Guid albumId)
	{
		var album = await _unitOfWork.Albums.GetByIdAsync(albumId);
		Validator.Validate(album);

		return AlbumDetailsDto.FromEntity(album);
	}

	public async Task<IEnumerable<AlbumDto>> GetAllAlbumsByArtistAsync(Guid artistId)
	{
		var artist = await _unitOfWork.Artists.GetByIdAsync(artistId);
		Validator.Validate(artist);

		var albums = await _unitOfWork.Albums.GetAllNoTrackingAsync()
			.Where(a => a.ArtistId == artistId)
			.ToListAsync();

		return albums.Select(AlbumDto.FromEntity);
	}

	public async Task<IEnumerable<AlbumDto>> GetAllAlbumsByPartialTitleAsync(string partialTitle)
	{
		if (string.IsNullOrWhiteSpace(partialTitle))
			throw new ArgumentException("Partial title cannot be empty.", nameof(partialTitle));

		var albums = await _unitOfWork.Albums.GetAllNoTrackingAsync()
			.Where(a => a.Title.Contains(partialTitle))
			.ToListAsync();

		return albums.Select(AlbumDto.FromEntity);
	}

	public async Task AddSongToAlbumAsync(Guid albumId, Guid songId)
	{
		var album = await _unitOfWork.Albums.GetByIdAsync(albumId);
		Validator.Validate(album);
		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		Validator.Validate(song);

		if (album.Songs.Any(s => s.Id == songId))
			throw new InvalidOperationException("Song already exists in the album.");
		
		album.Songs.Add(song);
		await _unitOfWork.BeginTransactionAsync();
		_unitOfWork.Albums.Update(album);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task UpdateAlbumAsync(AlbumUpdateDto albumUpdateDto)
	{
		if (string.IsNullOrWhiteSpace(albumUpdateDto.Title))
			throw new ArgumentException("Album title cannot be empty.", nameof(albumUpdateDto.Title));

		var album = await _unitOfWork.Albums.GetByIdAsync(albumUpdateDto.Id);
		Validator.Validate(album);

		album.Title = albumUpdateDto.Title ?? album.Title;
		album.Description = albumUpdateDto.Description ?? album.Description;

		await _unitOfWork.BeginTransactionAsync();
		_unitOfWork.Albums.Update(album);
		await _unitOfWork.CommitTransactionAsync();
	}


	public async Task DeleteAlbumAsync(Guid albumId)
	{
		var album = await _unitOfWork.Albums.GetByIdAsync(albumId);
		Validator.Validate(album);

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Albums.DeleteAsync(albumId);
		await _unitOfWork.CommitTransactionAsync();
	}


}
