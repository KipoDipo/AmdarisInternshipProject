using HiFive.Application.DTOs.Artist;
using HiFive.Application.DTOs.Listener;

namespace HiFive.Application.Contracts.Services.Contracts;

public interface IListenerService
{
	Task<ListenerDto> CreateListenerAsync(ListenerCreateDto listenerCreateDto);

	Task LikeSongAsync(Guid listenerId, Guid songId);
	Task UnlikeSongAsync(Guid listenerId, Guid songId);
	Task FollowArtistAsync(Guid listenerId, Guid artistId);
	Task UnfollowArtistAsync(Guid listenerId, Guid artistId);
	Task FollowListenerAsync(Guid followerId, Guid followeeId);
	Task UnfollowListenerAsync(Guid followerId, Guid followeeId);

	Task<ListenerDto> GetListenerByIdAsync(Guid listenerId);
	Task<IEnumerable<ArtistDto>> GetFollowingArtists(Guid listenerId);
	Task<IEnumerable<ListenerDto>> GetFollowingListeners(Guid listenerId);
	Task<IEnumerable<ListenerDto>> GetListenersByPartialNameAsync(string partialName);
	Task<ListenerDetailsDto> GetListenerDetailsByIdAsync(Guid listenerId);

	Task UpdateListenerAsync(ListenerUpdateDto listenerUpdateDto);
	Task SubscribeListener(Guid listenerId);
	Task UnsubscribeListener(Guid listenerId);
}
