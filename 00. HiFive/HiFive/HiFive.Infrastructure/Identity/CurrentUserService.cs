using HiFive.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class CurrentUserService : ICurrentUserService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public CurrentUserService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public Guid Id
	{
		get
		{
			var token = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (token == null)
				throw new UnauthorizedException("Token is wrong or has expired");
			return Guid.Parse(token);
		}
	}
}