namespace HiFive.Application.Contracts.Services.Contracts;

public interface IListenerDataService
{
	Task AddListenedSong(Guid listenerId, Guid songId);
}
