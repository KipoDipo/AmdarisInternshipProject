using HiFive.Application.Exceptions;

namespace HiFive.Infrastructure.Exceptions;
public class UnauthorizedException : HiFiveException
{
	public UnauthorizedException(string message) : base(message)
	{
	}
}
