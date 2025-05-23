using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class GenreRepository : BaseRepository<Genre>, IGenreRepository
{
	public GenreRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
