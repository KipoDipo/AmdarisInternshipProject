using HiFive.Application.Contracts;
using HiFive.Domain.Contracts;
using HiFive.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure;

public class Repository<T> : IRepository<T> where T : class, IDeletable
{
	private readonly DbContext _dbContext;

	public Repository(DbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task AddAsync(T entity)
	{
		await _dbContext.AddAsync(entity);
	}

	public IQueryable<T> GetAllAsync()
	{
		return _dbContext.Set<T>().AsQueryable();
	}

	public IQueryable<T> GetAllNoTrackingAsync()
	{
		return _dbContext.Set<T>().AsNoTracking().AsQueryable();
	}

	// Will use Tracker
	public async Task<T?> GetByIdAsync(Guid id)
	{
		return await _dbContext.Set<T>().FindAsync(id);
	}

	public void Update(T entity)
	{
		entity.UpdatedOn = DateTime.Now;
		_dbContext.Update(entity);
	}

	public async Task DeleteAsync(Guid id)
	{
		var entity = await _dbContext.Set<T>().FindAsync(id);
		Validator.Validate(entity);

		entity.IsDeleted = true;
		entity.DeletedOn = DateTime.Now;
	}
}
