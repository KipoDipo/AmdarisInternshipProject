using HiFive.Domain.Contracts;

namespace HiFive.Application.Contracts;

public interface IRepository<T> where T : IBase
{
	Task<T?> GetByIdAsync(Guid id);
	IQueryable<T> GetAll();
	IQueryable<T> GetAllNoTracking();
	Task AddAsync(T entity);
	void Update(T entity);
	Task DeleteAsync(Guid id);
}
