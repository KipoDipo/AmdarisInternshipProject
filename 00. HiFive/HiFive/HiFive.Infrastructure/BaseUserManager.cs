using HiFive.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace HiFive.Infrastructure;

public class BaseUserManager<T> where T : ApplicationUser, new()
{
	private readonly UserManager<ApplicationUser> _userManager;

	public IQueryable<T> Users => _userManager.Users.OfType<T>();

	public BaseUserManager(UserManager<ApplicationUser> userManager)
	{
		_userManager = userManager;
	}

	public async Task<T> CreateAsync(T entity, string password)
	{
		if (entity.DisplayName == null)
			throw new ArgumentException("User display name cannot be empty.", nameof(entity.DisplayName));

		var result = await _userManager.CreateAsync(entity, password);
		if (!result.Succeeded)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (var e in result.Errors)
			{
				stringBuilder.AppendLine($"Code: {e.Code}");
				stringBuilder.AppendLine($"Description: {e.Description}");
			}
			throw new Exception($"User creation failed.\n" +
				$"{stringBuilder.ToString()}");
		}
		entity.CreatedOn = DateTime.UtcNow;
		entity.UpdatedOn = DateTime.UtcNow;
		return entity;
	}

	public async Task<T> UpdateAsync(T entity)
	{
		var result = await _userManager.UpdateAsync(entity);
		if (!result.Succeeded)
			throw new Exception("User update failed.");
		entity.UpdatedOn = DateTime.UtcNow;
		return entity;
	}
}
