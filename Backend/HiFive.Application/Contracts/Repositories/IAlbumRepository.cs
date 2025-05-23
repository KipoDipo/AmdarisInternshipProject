using HiFive.Application.Contracts.Repositories.Helpers;
using HiFive.Domain.Models.Music;

namespace HiFive.Application.Contracts.Repositories;

public interface IAlbumRepository : IRepository<Album>, IGettableWithDetails<Album>
{
}
