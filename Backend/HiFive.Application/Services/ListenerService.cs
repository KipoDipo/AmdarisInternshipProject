using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Artist;
using HiFive.Application.DTOs.Listener;
using HiFive.Application.Exceptions;
using HiFive.Application.UnitOfWork;
using HiFive.Domain.Models.Join;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Application.Services;

public class ListenerService : IListenerService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IValidator _validator;

	public ListenerService(IUnitOfWork unitOfWork, IValidator validator)
	{
		_unitOfWork = unitOfWork;
		_validator = validator;
	}

	public async Task<ListenerDto> CreateListenerAsync(ListenerCreateDto listenerCreateDto)
	{
		if (string.IsNullOrWhiteSpace(listenerCreateDto.DisplayName))
			throw new UserInputException("Listener display name cannot be empty.");

		await _unitOfWork.BeginTransactionAsync();
		var listener = await _unitOfWork.Listeners.Register(listenerCreateDto);
		await _unitOfWork.CommitTransactionAsync();

		return ListenerDto.FromEntity(listener);
	}
	
	public async Task LikeSongAsync(Guid listenerId, Guid songId)
	{
		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		_validator.Validate(song);

		var listener = await _unitOfWork.Listeners.GetAll()
			.Include(l => l.LikedSongs)
			.Where(l => l.Id == listenerId)
			.FirstOrDefaultAsync();
		_validator.Validate(listener);

		await _unitOfWork.BeginTransactionAsync();

		var maxOrderIndex = listener.LikedSongs.Count();

		var likedSong = new ListenerLikedSong()
		{
			Listener = listener,
			LikedSong = song,
			OrderIndex = maxOrderIndex
		};

		listener.LikedSongs.Add(likedSong);

		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task UnlikeSongAsync(Guid listenerId, Guid songId)
	{
		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		_validator.Validate(song);

		var listener = await _unitOfWork.Listeners.GetAll()
			.Include(l => l.LikedSongs)
			.Where(l => l.Id == listenerId)
			.FirstOrDefaultAsync();
		_validator.Validate(listener);

		await _unitOfWork.BeginTransactionAsync();

		var record = listener.LikedSongs.FirstOrDefault(s => s.LikedSongId == songId);
		if (record == null)
			throw new NotFoundException("Record not found");

		listener.LikedSongs.Remove(record);
		var index = 1;
		foreach (var listenerLikedSong in listener.LikedSongs.OrderBy(ps => ps.OrderIndex))
		{
			listenerLikedSong.OrderIndex = index++;
		}

		await _unitOfWork.Listeners.UpdateAsync(listener);

		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task SubscribeListener(Guid listenerId)
	{
		var listener = await _unitOfWork.Listeners.GetByIdAsync(listenerId);
		_validator.Validate(listener);

		await _unitOfWork.BeginTransactionAsync();
		listener.IsSubscribed = true;
		listener.SubscriptionEndDate = DateTime.Now.AddMonths(1);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task UnsubscribeListener(Guid listenerId)
	{
		var listener = await _unitOfWork.Listeners.GetByIdAsync(listenerId);
		_validator.Validate(listener);

		await _unitOfWork.BeginTransactionAsync();
		listener.IsSubscribed = false;
		listener.SubscriptionEndDate = null;
		await _unitOfWork.CommitTransactionAsync();
	}


	public async Task<ListenerDto> GetListenerByIdAsync(Guid listenerId)
	{
		var listener = await _unitOfWork.Listeners.GetByIdAsync(listenerId);
		_validator.Validate(listener);

		return ListenerDto.FromEntity(listener);
	}

	public async Task<ListenerDetailsDto> GetListenerDetailsByIdAsync(Guid listenerId)
	{
		var listener = await _unitOfWork.Listeners.GetWithDetailsByIdAsync(listenerId);
		_validator.Validate(listener);

		return ListenerDetailsDto.FromEntity(listener);
	}

	public async Task<IEnumerable<ListenerDto>> GetListenersByPartialNameAsync(string partialName)
	{
		var listeners = await _unitOfWork.Listeners.GetAllByPartialName(partialName);

		return listeners.Select(ListenerDto.FromEntity).ToList();
	}


	public async Task UpdateListenerAsync(ListenerUpdateDto listenerUpdateDto)
	{
		var listener = await _unitOfWork.Listeners.GetByIdAsync(listenerUpdateDto.Id);
		_validator.Validate(listener);

		await _unitOfWork.BeginTransactionAsync();

		listener.DisplayName = listenerUpdateDto.DisplayName ?? listener.DisplayName;
		listener.FirstName = listenerUpdateDto.FirstName;
		listener.LastName = listenerUpdateDto.LastName;
		listener.Bio = listenerUpdateDto.Bio;
		listener.ProfilePictureId = listenerUpdateDto.ProfilePictureId ?? listener.ProfilePictureId;
		listener.EquippedBadgeId = listenerUpdateDto.EquippedBadgeId ?? listener.EquippedBadgeId;
		listener.EquippedTitleId = listenerUpdateDto.EquippedTitleId ?? listener.EquippedTitleId;

		await _unitOfWork.Listeners.UpdateAsync(listener);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task FollowArtistAsync(Guid listenerId, Guid artistId)
	{
		var listener = await _unitOfWork.Listeners.GetWithDetailsByIdAsync(listenerId);
		_validator.Validate(listener);

		var artist = await _unitOfWork.Artists.GetByIdAsync(artistId);
		_validator.Validate(artist);

		await _unitOfWork.BeginTransactionAsync();

		listener.FollowingArtists.Add(artist);

		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task UnfollowArtistAsync(Guid listenerId, Guid artistId)
	{
		var listener = await _unitOfWork.Listeners.GetWithDetailsByIdAsync(listenerId);
		_validator.Validate(listener);

		var artist = await _unitOfWork.Artists.GetByIdAsync(artistId);
		_validator.Validate(artist);

		var followedArtist = listener.FollowingArtists.FirstOrDefault(a => a.Id == artistId);
		_validator.Validate(followedArtist);

		await _unitOfWork.BeginTransactionAsync();

		listener.FollowingArtists.Remove(followedArtist);

		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task<IEnumerable<ArtistDto>> GetFollowingArtists(Guid listenerId)
	{
		var listener = await _unitOfWork.Listeners.GetWithDetailsByIdAsync(listenerId);
		_validator.Validate(listener);

		return listener.FollowingArtists.Select(ArtistDto.FromEntity);
	}
}
