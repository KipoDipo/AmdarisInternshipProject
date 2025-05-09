using HiFive.Application.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace HiFive.Infrastructure.Exceptions;

public class IdentityCreationException : HiFiveException
{
	public IEnumerable<IdentityError> Errors { get; }

	public IdentityCreationException(IEnumerable<IdentityError> errors)
		: base("User creation failed")
	{
		Errors = errors;
	}
}
