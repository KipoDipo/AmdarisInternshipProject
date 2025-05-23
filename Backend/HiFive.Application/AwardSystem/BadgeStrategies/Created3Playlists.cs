namespace HiFive.Application.AwardSystem.BadgeStrategies;

public class Created3Playlists : TrophyStrategyBase
{
	public override string ConditionKey => Conditions.Playlists.Created3;
}