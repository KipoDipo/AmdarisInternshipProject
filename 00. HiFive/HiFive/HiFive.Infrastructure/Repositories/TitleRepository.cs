using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Throphies;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class TitleRepository : BaseRepository<Title>, ITitleRepository
{
	public TitleRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public override Task<Title?> GetWithDetailsByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}
}
