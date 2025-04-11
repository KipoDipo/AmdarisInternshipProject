using HiFive.Application.Contracts;
using HiFive.Application.DTOs.Playlist;
using HiFive.Application.DTOs.Song;
using HiFive.Application.Services.Contracts;
using HiFive.Domain.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Application.Services;

public class PlaylistService : IPlaylistService
{
	private readonly IUnitOfWork _unitOfWork;

	public PlaylistService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<PlaylistDto> CreatePlaylistAsync(string name, string description, Guid userId)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Playlist name cannot be empty.", nameof(name));

		var user = await _unitOfWork.Listeners.GetByIdNoTrackingAsync(userId);
		Validator.Validate(user);

		var playlist = new Playlist
		{
			Title = name,
			Description = description,
			OwnerId = userId,
		};

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Playlists.AddAsync(playlist);
		await _unitOfWork.CommitTransactionAsync();

		return PlaylistDto.FromEntity(playlist);
	}

	public async Task<PlaylistDto> GetPlaylistByIdAsync(Guid playlistId)
	{
		var playlist = await _unitOfWork.Playlists.GetByIdNoTrackingAsync(playlistId);

		Validator.Validate(playlist);

		return PlaylistDto.FromEntity(playlist);
	}

	public async Task<IEnumerable<PlaylistDto>> GetPlaylistsByUserIdAsync(Guid userId)
	{
		var user = await _unitOfWork.Listeners.GetByIdNoTrackingAsync(userId);
		Validator.Validate(user);

		var playlists = await _unitOfWork.Playlists.GetAllNoTrackingAsync()
			.Where(p => p.OwnerId == userId)
			.ToListAsync();

		return playlists.Select(PlaylistDto.FromEntity);
	}

	public async Task UpdatePlaylistAsync(Guid playlistId, string name, string description)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Playlist name cannot be empty.", nameof(name));

		var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
		Validator.Validate(playlist);

		playlist.Title = name;
		playlist.Description = description;

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Playlists.UpdateAsync(playlist);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task DeletePlaylistAsync(Guid playlistId)
	{
		var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
		Validator.Validate(playlist);

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Playlists.DeleteAsync(playlistId);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task AddSongToPlaylistAsync(Guid playlistId, Guid songId)
	{
		var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
		Validator.Validate(playlist);

		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		Validator.Validate(song);

		playlist.Songs.Add(song);

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Playlists.UpdateAsync(playlist);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task RemoveSongFromPlaylistAsync(Guid playlistId, Guid songId)
	{
		var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
		Validator.Validate(playlist);

		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		Validator.Validate(song);

		playlist.Songs.Remove(song);

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Playlists.UpdateAsync(playlist);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task<IEnumerable<SongDto>> GetSongsInPlaylistAsync(Guid playlistId)
	{
		var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
		Validator.Validate(playlist);

		var songs = await _unitOfWork.Songs.GetAllNoTrackingAsync()
			.Where(s => playlist.Songs.Contains(s) && !s.IsDeleted) // TODO: Query Filters
			.ToListAsync();
		
		return songs.Select(SongDto.FromEntity);
	}

}
