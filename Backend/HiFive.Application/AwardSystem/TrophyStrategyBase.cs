using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Domain.Models.Users;

namespace HiFive.Application.AwardSystem;

public abstract class TrophyStrategyBase : ITrophyStrategy
{
	public abstract Task Execute(Guid listenerId, ITrophyService service);
}
