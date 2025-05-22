using HiFive.Application.Exceptions;
using HiFive.Application.UnitOfWork;
using HiFive.Domain.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace HiFive.Infrastructure;

public class Validator : IValidator
{
	public void Validate<T>([NotNull] T? entity) where T : IDeletable
	{
		if (entity == null)
			throw new NotFoundException($"{typeof(T).Name} was not found.");
		if (entity.IsDeleted)
			throw new NotFoundException($"{typeof(T).Name} is deleted.");
	}
}
