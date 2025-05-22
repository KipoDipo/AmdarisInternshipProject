using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Domain.Models.Users;

namespace HiFive.Application.AwardSystem;

public interface ITrophyStrategy
{
	Task Execute(Guid listenerId, ITrophyService service);
}
