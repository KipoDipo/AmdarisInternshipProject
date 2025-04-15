using HiFive.Domain.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace HiFive.Infrastructure.Services;

internal class Validator
{
	public static void Validate<T>([NotNull] T? entity) where T : IDeletable
	{
		if (entity == null)
			throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} was not found.");
		if (entity.IsDeleted)
			throw new InvalidOperationException($"{nameof(entity)} is deleted.");
	}
}
