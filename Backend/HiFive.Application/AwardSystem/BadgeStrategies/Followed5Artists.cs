namespace HiFive.Application.AwardSystem.BadgeStrategies;

public class Followed5Artists : TrophyStrategyBase
{
	public override string ConditionKey => Conditions.Artists.Followed5;
}
