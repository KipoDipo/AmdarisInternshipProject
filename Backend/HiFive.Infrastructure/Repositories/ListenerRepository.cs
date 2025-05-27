using HiFive.Application.Contracts.Repositories;
using HiFive.Application.DTOs.Listener;
using HiFive.Domain.Models.Users;
using HiFive.Infrastructure.Exceptions;
using HiFive.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class ListenerRepository : BaseRepository<Listener>, IListenerRepository
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole<Guid>> _roleManager;

	public ListenerRepository(DbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager) : base(dbContext)
	{
		_userManager = userManager;
		_roleManager = roleManager;
	}

	public async Task<IEnumerable<Listener>> GetAllByPartialName(string partialName)
	{
		return await _dbContext.Set<Listener>()
			.Where(l => l.DisplayName.Contains(partialName))
			.ToListAsync();
	}

	public async Task<Listener?> GetWithDetailsByIdAsync(Guid id)
	{
		return await _dbContext.Set<Listener>()
			.Include(l => l.CreatedPlaylists)
			.Include(l => l.Badges)
			.ThenInclude(lb => lb.Badge)
			.Include(l => l.Titles)
			.ThenInclude(lt => lt.Title)
			.Include(l => l.FollowingArtists)
			.Include(l => l.FollowingListeners)
			.Include(l => l.LikedSongs)
			.Include(l => l.LikedPlaylists)
			.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<Listener> Register(ListenerCreateDto listenerCreateDto)
	{
		ApplicationUser newListener = new()
		{
			UserName = listenerCreateDto.UserName,
			Email = listenerCreateDto.Email,
			PhoneNumber = listenerCreateDto.PhoneNumber,
		};

		if (!await _roleManager.RoleExistsAsync("Listener"))
		{
			await _roleManager.CreateAsync(new IdentityRole<Guid>("Listener"));
		}

		var result = await _userManager.CreateAsync(newListener, listenerCreateDto.Password);

		if (!result.Succeeded)
			throw new IdentityCreationException(result.Errors);

		await _userManager.AddToRoleAsync(newListener, "Listener");

		var listener = new Listener()
		{
			Id = newListener.Id,
			DisplayName = listenerCreateDto.DisplayName,
			FirstName = listenerCreateDto.FirstName,
			LastName = listenerCreateDto.LastName,
			Bio = listenerCreateDto.Bio,
			ProfilePictureId = listenerCreateDto.ProfilePictureId,
		};
		await _dbContext.AddAsync(listener);

		return listener;
	}
}
