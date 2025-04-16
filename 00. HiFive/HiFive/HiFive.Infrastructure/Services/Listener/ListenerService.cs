using HiFive.Application.DTOs.Listener;
using HiFive.Application.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Services.Listener;

public class ListenerService : IListenerService
{
	private readonly BaseUserManager<Domain.Models.Users.Listener> _userManager;

	public ListenerService(BaseUserManager<Domain.Models.Users.Listener> userManager)
	{
		_userManager = userManager;
	}

	public async Task<ListenerDto> CreateListenerAsync(ListenerCreateDto listenerCreateDto)
	{
		if (string.IsNullOrWhiteSpace(listenerCreateDto.DisplayName))
			throw new ArgumentException("Listener display name cannot be empty.", nameof(listenerCreateDto.DisplayName));

		var listener = new Domain.Models.Users.Listener()
		{
			UserName = listenerCreateDto.UserName,
			DisplayName = listenerCreateDto.DisplayName,
			FirstName = listenerCreateDto.FirstName,
			LastName = listenerCreateDto.LastName,
			Email = listenerCreateDto.Email,
			Bio = listenerCreateDto.Bio,
			ProfilePicture = listenerCreateDto.ProfilePicture,
		};
		await _userManager.CreateAsync(listener, listenerCreateDto.Password);
		return ListenerDto.FromEntity(listener);
	}

	public async Task<ListenerDto> GetListenerByIdAsync(Guid listenerId)
	{
		var listener = await _userManager.Users
			.FirstOrDefaultAsync(a => a.Id == listenerId);
		Validator.Validate(listener);

		return ListenerDto.FromEntity(listener);
	}

	public async Task<ListenerDetailsDto> GetListenerDetailsByIdAsync(Guid listenerId)
	{
		var listener = await _userManager.Users
			.Include(a => a.LikedSongs)
			.Include(a => a.CreatedPlaylists)
			.FirstOrDefaultAsync(a => a.Id == listenerId);

		Validator.Validate(listener);
		return ListenerDetailsDto.FromEntity(listener);
	}

	public async Task<IEnumerable<ListenerDto>> GetListenersByPartialNameAsync(string partialName)
	{
		var listeners = await _userManager.Users
			.Where(a => a.DisplayName.Contains(partialName))
			.ToListAsync();

		return listeners.Select(ListenerDto.FromEntity).ToList();
	}

	public async Task<ListenerDto> UpdateListenerAsync(ListenerUpdateDto listenerUpdateDto)
	{
		var listener = await _userManager.Users
			.FirstOrDefaultAsync(a => a.Id == listenerUpdateDto.Id);
		Validator.Validate(listener);

		listener.DisplayName = listenerUpdateDto.DisplayName ?? listener.DisplayName;
		listener.FirstName = listenerUpdateDto.FirstName ?? listener.FirstName;
		listener.LastName = listenerUpdateDto.LastName ?? listener.LastName;
		listener.Bio = listenerUpdateDto.Bio ?? listener.Bio;
		listener.ProfilePicture = listenerUpdateDto.ProfilePicture ?? listener.ProfilePicture;

		await _userManager.UpdateAsync(listener);

		return ListenerDto.FromEntity(listener);
	}
}
