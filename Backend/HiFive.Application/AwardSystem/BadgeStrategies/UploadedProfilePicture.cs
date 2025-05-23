namespace HiFive.Application.AwardSystem.BadgeStrategies;

public class UploadedProfilePicture : TrophyStrategyBase
{
	public override string ConditionKey { get => Conditions.Profile.UploadedPfp; }
}
