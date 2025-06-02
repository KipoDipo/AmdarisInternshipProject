using HiFive.Application.Contracts.Repositories;
using HiFive.Application.DTOs.Distributor;
using HiFive.Domain.Models.Users;
using HiFive.Infrastructure.Exceptions;
using HiFive.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class DistributorRepository : BaseRepository<Distributor>, IDistributorRepository
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole<Guid>> _roleManager;

	public DistributorRepository(DbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager) : base(dbContext)
	{
		_userManager = userManager;
		_roleManager = roleManager;
	}

	public async Task<IEnumerable<Distributor>> GetAllByPartialName(string partialName)
	{
		return await _dbContext.Set<Distributor>()
			.Where(a => a.DisplayName.Contains(partialName))
			.ToListAsync();
	}

	public async Task<Distributor?> GetWithDetailsByIdAsync(Guid id)
	{
		return await _dbContext.Set<Distributor>()
			.Include(x => x.Artists)
			.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<Distributor> Register(DistributorCreateDto dto)
	{
		ApplicationUser newDistributor = new()
		{
			UserName = dto.UserName,
			Email = dto.Email,
			PhoneNumber = dto.PhoneNumber,
		};

		if (!await _roleManager.RoleExistsAsync("Distributor"))
		{
			await _roleManager.CreateAsync(new IdentityRole<Guid>("Distributor"));
		}

		var result = await _userManager.CreateAsync(newDistributor, dto.Password);

		if (!result.Succeeded)
			throw new IdentityCreationException(result.Errors);

		await _userManager.AddToRoleAsync(newDistributor, "Distributor");

		var distributor = new Distributor()
		{
			Id = newDistributor.Id,
			DisplayName = dto.DisplayName,
			FirstName = dto.FirstName,
			LastName = dto.LastName,
			Bio = dto.Bio,
			ProfilePictureId = dto.ProfilePictureId,
		};

		await _dbContext.AddAsync(distributor);
		return distributor;
	}
}
