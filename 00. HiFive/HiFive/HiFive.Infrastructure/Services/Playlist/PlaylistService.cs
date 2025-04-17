﻿using HiFive.Application.Contracts;
using HiFive.Application.DTOs.Playlist;
using HiFive.Application.DTOs.Song;
using HiFive.Application.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Services.Playlist;

public class PlaylistService : IPlaylistService
{
	private readonly IUnitOfWork _unitOfWork;

	public PlaylistService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<PlaylistDto> CreatePlaylistAsync(PlaylistCreateDto playlistCreateDto)
	{
		if (string.IsNullOrWhiteSpace(playlistCreateDto.Title))
			throw new ArgumentException("Playlist name cannot be empty.", nameof(playlistCreateDto.Title));

		var user = await _unitOfWork.Listeners.GetByIdAsync(playlistCreateDto.OwnerId);
		Validator.Validate(user);

		var playlist = new Domain.Models.Music.Playlist
		{
			Title = playlistCreateDto.Title,
			Description = playlistCreateDto.Description,
			OwnerId = playlistCreateDto.OwnerId,
		};

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Playlists.AddAsync(playlist);
		await _unitOfWork.CommitTransactionAsync();

		return PlaylistDto.FromEntity(playlist);
	}

	public async Task<PlaylistDto> GetPlaylistByIdAsync(Guid playlistId)
	{
		var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
		Validator.Validate(playlist);

		return PlaylistDto.FromEntity(playlist);
	}

	public async Task<PlaylistDetailsDto> GetPlaylistDetailsByIdAsync(Guid playlistId)
	{
		var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
		Validator.Validate(playlist);

		return PlaylistDetailsDto.FromEntity(playlist);
	}

	public async Task<IEnumerable<PlaylistDto>> GetPlaylistsByUserIdAsync(Guid userId)
	{
		var user = await _unitOfWork.Listeners.GetByIdAsync(userId);
		Validator.Validate(user);

		var playlists = await _unitOfWork.Playlists.GetAllNoTracking()
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

		playlist.Title = name ?? playlist.Title;
		playlist.Description = description ?? playlist.Description;

		await _unitOfWork.BeginTransactionAsync();
		_unitOfWork.Playlists.Update(playlist);
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
		_unitOfWork.Playlists.Update(playlist);
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
		_unitOfWork.Playlists.Update(playlist);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task<IEnumerable<SongDto>> GetSongsInPlaylistAsync(Guid playlistId)
	{
		var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
		Validator.Validate(playlist);

		var songs = await _unitOfWork.Songs.GetAllNoTracking()
			.Where(s => playlist.Songs.Contains(s) && !s.IsDeleted) // TODO: Query Filters
			.ToListAsync();
		
		return songs.Select(SongDto.FromEntity);
	}
}
