using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Playlist;
using HiFive.Application.DTOs.Song;
using HiFive.Application.Exceptions;
using HiFive.Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Application.Services;

public class PlaylistService : IPlaylistService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IValidator _validator;

	public PlaylistService(IUnitOfWork unitOfWork, IValidator validator)
	{
		_unitOfWork = unitOfWork;
		_validator = validator;
	}

	public async Task<PlaylistDto> CreatePlaylistAsync(PlaylistCreateDto playlistCreateDto)
	{
		if (string.IsNullOrWhiteSpace(playlistCreateDto.Title))
			throw new UserInputException("Playlist name cannot be empty.");

		var user = await _unitOfWork.Listeners.GetByIdAsync(playlistCreateDto.OwnerId);
		_validator.Validate(user);

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
		_validator.Validate(playlist);

		return PlaylistDto.FromEntity(playlist);
	}

	public async Task<PlaylistDetailsDto> GetPlaylistDetailsByIdAsync(Guid playlistId)
	{
		var playlist = await _unitOfWork.Playlists.GetWithDetailsByIdAsync(playlistId);
		_validator.Validate(playlist);

		return PlaylistDetailsDto.FromEntity(playlist);
	}

	public async Task<IEnumerable<PlaylistDto>> GetPlaylistsByUserIdAsync(Guid userId)
	{
		var user = await _unitOfWork.Listeners.GetByIdAsync(userId);
		_validator.Validate(user);

		var playlists = await _unitOfWork.Playlists.GetAllNoTracking()
			.Where(p => p.OwnerId == userId)
			.ToListAsync();

		return playlists.Select(PlaylistDto.FromEntity);
	}

	public async Task UpdatePlaylistAsync(PlaylistUpdateDto playlistUpdateDto)
	{
		if (string.IsNullOrWhiteSpace(playlistUpdateDto.Title))
			throw new UserInputException("Playlist title cannot be empty.");

		var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistUpdateDto.Id);
		_validator.Validate(playlist);

		await _unitOfWork.BeginTransactionAsync();

		playlist.Title = playlistUpdateDto.Title ?? playlist.Title;
		playlist.Description = playlistUpdateDto.Description ?? playlist.Description;

		await _unitOfWork.Playlists.UpdateAsync(playlist);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task DeletePlaylistAsync(Guid playlistId)
	{
		var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
		_validator.Validate(playlist);

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Playlists.DeleteAsync(playlistId);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task AddSongToPlaylistAsync(Guid playlistId, Guid songId)
	{
		var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
		_validator.Validate(playlist);

		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		_validator.Validate(song);

		await _unitOfWork.BeginTransactionAsync();
		playlist.Songs.Add(song);

		await _unitOfWork.Playlists.UpdateAsync(playlist);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task RemoveSongFromPlaylistAsync(Guid playlistId, Guid songId)
	{
		var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
		_validator.Validate(playlist);

		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		_validator.Validate(song);

		await _unitOfWork.BeginTransactionAsync();
		playlist.Songs.Remove(song);

		await _unitOfWork.Playlists.UpdateAsync(playlist);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task<IEnumerable<SongDto>> GetSongsInPlaylistAsync(Guid playlistId)
	{
		var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
		_validator.Validate(playlist);

		var songs = await _unitOfWork.Songs.GetAllNoTracking()
			.Where(s => playlist.Songs.Contains(s) && !s.IsDeleted) // TODO: Query Filters
			.ToListAsync();

		return songs.Select(SongDto.FromEntity);
	}
}
