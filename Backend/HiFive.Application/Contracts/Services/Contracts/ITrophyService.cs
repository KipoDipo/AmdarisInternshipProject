using HiFive.Application.DTOs.Trophy;
using HiFive.Domain.Models.Throphies;

namespace HiFive.Application.Contracts.Services.Contracts;
public interface ITrophyService
{
	Task CreateBadge(BadgeCreateDto badgeCreateDto);
	Task CreateTitle(TitleCreateDto titleCreateDto);
	Task CreateCondition(ConditionCreateDto conditionCreateDto);

	Task AwardBadge(Guid listenerId, Guid badgeId);
	Task AwardTitle(Guid listenerId, Guid titleId);

	Task<IEnumerable<ListenerBadgeDto>> GetListenerBadges(Guid listenerId);

	Task<BadgeDto> GetBadgeById(Guid badgeId);
	Task<TitleDto> GetTitleById(Guid titleId);
	
	Task<BadgeDto> GetBadgeByConditionKeyAndArtist(string conditionKey, Guid? artistId = null);
	Task<Condition> GetConditionByKey(string conditionKey);
	Task<TitleDto> GetTitleByConditionKeyAndArtist(string conditionKey, Guid? artistId = null);
}
