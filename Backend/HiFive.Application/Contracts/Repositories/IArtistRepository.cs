using HiFive.Application.Contracts.Repositories.Helpers;
using HiFive.Application.DTOs.Artist;
using HiFive.Domain.Models.Users;

namespace HiFive.Application.Contracts.Repositories;

public interface IArtistRepository : IRepository<Artist>, IRegisterable<Artist, ArtistCreateDto>, IGettableByPartialName<Artist>
{
}