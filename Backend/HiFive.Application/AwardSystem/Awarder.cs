using HiFive.Application.Contracts.Services.Contracts;

namespace HiFive.Application.AwardSystem;

public class Awarder
{
	private readonly ITrophyService _trophyService;

	public Awarder(ITrophyService trophyService)
	{
		_trophyService = trophyService;
	}

	public async Task Award(Guid listenerId, ITrophyStrategy strategy)
	{
		await strategy.Execute(listenerId, _trophyService);
	}
}
