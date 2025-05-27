using HiFive.Application.DTOs.Song;

namespace HiFive.Application.Contracts.Services.Contracts;

public interface IListenerDataService
{
	Task AddListenedSong(Guid listenerId, Guid songId);
	Task<IEnumerable<SongDto>> GetUniqueSongsListenedById(Guid listenerId, int count = 8, int forTheLastNMonths = 1);
}
