using HiFive.Application.UnitOfWork;
using HiFive.Domain.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace HiFive.Infrastructure;

public class Validator : IValidator
{
	public void Validate<T>([NotNull] T? entity) where T : IDeletable
	{
		if (entity == null)
			throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} was not found.");
		if (entity.IsDeleted)
			throw new InvalidOperationException($"{nameof(entity)} is deleted.");
	}
}
