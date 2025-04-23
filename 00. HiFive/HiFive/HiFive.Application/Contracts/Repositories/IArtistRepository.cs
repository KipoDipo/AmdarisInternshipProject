using HiFive.Application.Contracts.Repositories.Helpers;
using HiFive.Domain.Models.Users;

namespace HiFive.Application.Contracts.Repositories;

public interface IArtistRepository : IRepository<Artist>, IRegisterable<Artist>, IGettableByPartialName<Artist>
{
}