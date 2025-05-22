using HiFive.Application.Contracts.Services.Contracts;

namespace HiFive.Application.AwardSystem.BadgeStrategies;

public class Registered : TrophyStrategyBase
{
	public const string ConditionKey = "registered";

	public override async Task Execute(Guid listenerId, ITrophyService service)
	{
		var badge = await service.GetBadgeByConditionKeyAndArtist(ConditionKey);

		await service.AwardBadge(listenerId, badge.Id);
	}
}
