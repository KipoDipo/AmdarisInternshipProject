using HiFive.Application.Contracts.Repositories.Helpers;
using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Users;

namespace HiFive.Application.Contracts.Repositories;

public interface ISongRepository : IRepository<Song>, IGettableWithDetails<Song>, IGettableByPartialName<Song>
{
}
