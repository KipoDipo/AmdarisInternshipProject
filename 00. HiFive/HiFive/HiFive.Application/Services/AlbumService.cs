using HiFive.Application.Contracts;
using HiFive.Application.DTOs.Album;
using HiFive.Application.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Application.Services;

public class AlbumService : IAlbumService
{
	private readonly IUnitOfWork _unitOfWork;

	public AlbumService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<AlbumDto> CreateAlbumAsync(string title, DateTime releaseDate, Guid artistId)
	{
		if (string.IsNullOrWhiteSpace(title))
			throw new ArgumentException("Album title cannot be empty.", nameof(title));

		var artist = await _unitOfWork.Artists.GetByIdNoTrackingAsync(artistId);
		Validator.Validate(artist);

		var album = new Domain.Models.Music.Album
		{
			Title = title,
			ReleaseDate = releaseDate,
			ArtistId = artistId
		};
		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Albums.AddAsync(album);
		await _unitOfWork.CommitTransactionAsync();
		return AlbumDto.FromEntity(album);
	}

	public async Task<AlbumDto> GetAlbumByIdAsync(Guid albumId)
	{
		var album = await _unitOfWork.Albums.GetByIdNoTrackingAsync(albumId);
		Validator.Validate(album);

		return AlbumDto.FromEntity(album);
	}

	public async Task<IEnumerable<AlbumDto>> GetAllAlbumsByArtistAsync(Guid artistId)
	{
		var artist = await _unitOfWork.Artists.GetByIdNoTrackingAsync(artistId);
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
		var album = await _unitOfWork.Albums.GetByIdNoTrackingAsync(albumId);
		Validator.Validate(album);
		var song = await _unitOfWork.Songs.GetByIdNoTrackingAsync(songId);
		Validator.Validate(song);

		if (album.Songs.Any(s => s.Id == songId))
			throw new InvalidOperationException("Song already exists in the album.");
		
		album.Songs.Add(song);
		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Albums.UpdateAsync(album);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task UpdateAlbumAsync(Guid albumId, string title, DateTime releaseDate)
	{
		if (string.IsNullOrWhiteSpace(title))
			throw new ArgumentException("Album title cannot be empty.", nameof(title));

		var album = await _unitOfWork.Albums.GetByIdNoTrackingAsync(albumId);
		Validator.Validate(album);

		album.Title = title;
		album.ReleaseDate = releaseDate;

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Albums.UpdateAsync(album);
		await _unitOfWork.CommitTransactionAsync();
	}


	public async Task DeleteAlbumAsync(Guid albumId)
	{
		var album = await _unitOfWork.Albums.GetByIdNoTrackingAsync(albumId);
		Validator.Validate(album);

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Albums.DeleteAsync(albumId);
		await _unitOfWork.CommitTransactionAsync();
	}


}
