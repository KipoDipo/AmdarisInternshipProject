using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Trophy;
using HiFive.Application.UnitOfWork;
using HiFive.Domain.Models.Join;
using HiFive.Domain.Models.Throphies;
using HiFive.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Application.Services;

public class TrophyService : ITrophyService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IValidator _validator;

	public TrophyService(IUnitOfWork unitOfWork, IValidator validator)
	{
		_unitOfWork = unitOfWork;
		_validator = validator;
	}

	public async Task CreateCondition(ConditionCreateDto conditionCreateDto)
	{
		var condition = new Condition()
		{
			Key = conditionCreateDto.Key,
		};
		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Conditions.AddAsync(condition);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task CreateBadge(BadgeCreateDto badgeCreateDto)
	{
		var badge = new Badge()
		{
			Name = badgeCreateDto.Name,
			Description = badgeCreateDto.Description,
			ConditionId = badgeCreateDto.ConditionId,
			ImageId = badgeCreateDto.ImageId,
		};
		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Badges.AddAsync(badge);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task CreateTitle(TitleCreateDto titleCreateDto)
	{
		var title = new Title()
		{
			Name = titleCreateDto.Name,
			Description = titleCreateDto.Description,
			ConditionId = titleCreateDto.ConditionId,
		};
		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Titles.AddAsync(title);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task AwardBadge(Guid listenerId, Guid badgeId)
	{
		var listener = await _unitOfWork.Listeners.GetAll()
			.Include(x => x.Badges)
			.Where(x => x.Id == listenerId)
			.FirstOrDefaultAsync();
		_validator.Validate(listener);

		if (listener.Badges.Any(x => x.BadgeId == badgeId))
			return;

		var listenerBadge = new ListenerBadge()
		{
			Listener = listener,
			BadgeId = badgeId,
			AwardedAt = DateTime.Now
		};


		await _unitOfWork.BeginTransactionAsync();
		listener.Badges.Add(listenerBadge);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task AwardTitle(Guid listenerId, Guid titleId)
	{
		var listener = await _unitOfWork.Listeners.GetAll()
			.Include(x => x.Titles)
			.Where(x => x.Id == listenerId)
			.FirstOrDefaultAsync();
		_validator.Validate(listener);

		if (listener.Titles.Any(x => x.TitleId == titleId))
			return;

		var listenerTitle = new ListenerTitle()
		{
			Listener = listener,
			TitleId = titleId,
			AwardedAt = DateTime.Now
		};

		await _unitOfWork.BeginTransactionAsync();
		listener.Titles.Add(listenerTitle);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task<Condition> GetConditionByKey(string conditionKey)
	{
		var condition = await _unitOfWork.Conditions.GetAllNoTracking()
			.Where(c => c.Key == conditionKey)
			.FirstOrDefaultAsync();
		_validator.Validate(condition);
		return condition;
	}

	public async Task<BadgeDto> GetBadgeByConditionKeyAndArtist(string conditionKey, Guid? artistId = null)
	{
		var condition = await GetConditionByKey(conditionKey);

		var badge = await _unitOfWork.Badges.GetAllNoTracking()
			.Where(b => b.ConditionId == condition.Id && b.ArtistId == artistId)
			.FirstOrDefaultAsync();
		_validator.Validate(badge);

		return BadgeDto.FromEntity(badge);
	}

	public async Task<TitleDto> GetTitleByConditionKeyAndArtist(string conditionKey, Guid? artistId = null)
	{
		var condition = await GetConditionByKey(conditionKey);

		var title = await _unitOfWork.Titles.GetAllNoTracking()
			.Where(t => t.ConditionId == condition.Id && t.ArtistId == artistId)
			.FirstOrDefaultAsync();
		_validator.Validate(title);

		return TitleDto.FromEntity(title);
	}

	public async Task<BadgeDto> GetBadgeById(Guid badgeId)
	{
		var badge = await _unitOfWork.Badges.GetByIdAsync(badgeId);
		_validator.Validate(badge);

		return BadgeDto.FromEntity(badge);
	}

	public async Task<TitleDto> GetTitleById(Guid titleId)
	{
		var title = await _unitOfWork.Titles.GetByIdAsync(titleId);
		_validator.Validate(title);

		return TitleDto.FromEntity(title);
	}

	public async Task<IEnumerable<ListenerBadgeDto>> GetListenerBadges(Guid listenerId)
	{
		var listener = await _unitOfWork.Listeners.GetAll()
			.Include(x => x.Badges)
			.ThenInclude(x => x.Badge)
			.FirstOrDefaultAsync(x => x.Id == listenerId);

		_validator.Validate(listener);

		return listener.Badges.Select(ListenerBadgeDto.FromEntity);
	}
}
