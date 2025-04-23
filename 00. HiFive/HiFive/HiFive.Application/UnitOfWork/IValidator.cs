using HiFive.Domain.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace HiFive.Application.UnitOfWork;

public interface IValidator
{
	void Validate<T>([NotNull] T? entity) where T : IDeletable;
}
