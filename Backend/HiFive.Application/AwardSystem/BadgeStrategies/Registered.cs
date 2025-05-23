namespace HiFive.Application.AwardSystem.BadgeStrategies;

public class Registered : TrophyStrategyBase
{
	public override string ConditionKey => Conditions.Profile.Registered;
}
