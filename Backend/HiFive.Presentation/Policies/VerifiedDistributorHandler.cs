using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HiFive.Presentation.Policies;

public class VerifiedDistributorHandler : AuthorizationHandler<VerifiedDistributorRequirement>
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IDistributorService _distributorService;

	public VerifiedDistributorHandler(UserManager<ApplicationUser> userManager, IDistributorService distributorService)
	{
		_userManager = userManager;
		_distributorService = distributorService;
	}

	protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, VerifiedDistributorRequirement requirement)
	{
		var user = await _userManager.GetUserAsync(context.User);

		if (user == null)
			return;

		if (await _userManager.IsInRoleAsync(user, "Distributor"))
		{
			var distributor = await _distributorService.GetDistributorByIdAsync(user.Id);
			if (distributor.IsApproved)
				context.Succeed(requirement);
		}
		else if (await _userManager.IsInRoleAsync(user, "Admin"))
		{
			context.Succeed(requirement); // Admins bypass
		}
	}
}
