using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Statistics;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;
public class ListenerDataRepository : BaseRepository<ListenerData>, IListenerDataRepository
{
	public ListenerDataRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
