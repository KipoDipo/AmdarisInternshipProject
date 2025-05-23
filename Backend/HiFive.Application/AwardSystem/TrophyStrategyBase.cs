using HiFive.Application.Contracts.Services.Contracts;

namespace HiFive.Application.AwardSystem;

public abstract class TrophyStrategyBase : ITrophyStrategy
{
	public abstract string ConditionKey { get; }

	public virtual async Task Execute(Guid listenerId, ITrophyService service)
	{
		var badge = await service.GetBadgeByConditionKeyAndArtist(ConditionKey);

		await service.AwardBadge(listenerId, badge.Id);
	}
}
