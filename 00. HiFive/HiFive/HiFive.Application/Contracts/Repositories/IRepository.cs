using HiFive.Domain.Contracts;

namespace HiFive.Application.Contracts.Repositories;

public interface IRepository<T> where T : IBase
{
	Task<T?> GetByIdAsync(Guid id);
	Task<T?> GetWithDetailsByIdAsync(Guid id);
	IQueryable<T> GetAll();
	IQueryable<T> GetAllNoTracking();
	Task AddAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(Guid id);
}
