using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Throphies;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class ConditionRepository : BaseRepository<Condition>, IConditionRepository
{
	public ConditionRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public override Task<Condition?> GetWithDetailsByIdAsync(Guid id)
	{
		return GetByIdAsync(id);
	}
}
