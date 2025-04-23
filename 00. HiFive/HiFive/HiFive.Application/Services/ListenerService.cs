using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Listener;
using HiFive.Application.UnitOfWork;

namespace HiFive.Application.Services;

public class ListenerService : IListenerService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IValidator _validator;

	public ListenerService(IUnitOfWork unitOfWork, IValidator validator)
	{
		_unitOfWork = unitOfWork;
		_validator = validator;
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

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.Listeners.Register(listener, listenerCreateDto.Password);
		await _unitOfWork.CommitTransactionAsync();

		return ListenerDto.FromEntity(listener);
	}

	public async Task<ListenerDto> GetListenerByIdAsync(Guid listenerId)
	{
		var listener = await _unitOfWork.Listeners.GetByIdAsync(listenerId);
		_validator.Validate(listener);

		return ListenerDto.FromEntity(listener);
	}

	public async Task<ListenerDetailsDto> GetListenerDetailsByIdAsync(Guid listenerId)
	{
		var listener = await _unitOfWork.Listeners.GetWithDetailsByIdAsync(listenerId);
		_validator.Validate(listener);

		return ListenerDetailsDto.FromEntity(listener);
	}

	public async Task<IEnumerable<ListenerDto>> GetListenersByPartialNameAsync(string partialName)
	{
		var listeners = await _unitOfWork.Listeners.GetAllByPartialName(partialName);

		return listeners.Select(ListenerDto.FromEntity).ToList();
	}

	public async Task UpdateListenerAsync(ListenerUpdateDto listenerUpdateDto)
	{
		var listener = await _unitOfWork.Listeners.GetByIdAsync(listenerUpdateDto.Id);
		_validator.Validate(listener);

		await _unitOfWork.BeginTransactionAsync();

		listener.DisplayName = listenerUpdateDto.DisplayName ?? listener.DisplayName;
		listener.FirstName = listenerUpdateDto.FirstName ?? listener.FirstName;
		listener.LastName = listenerUpdateDto.LastName ?? listener.LastName;
		listener.Bio = listenerUpdateDto.Bio ?? listener.Bio;
		listener.ProfilePicture = listenerUpdateDto.ProfilePicture ?? listener.ProfilePicture;

		await _unitOfWork.Listeners.UpdateAsync(listener);
		await _unitOfWork.CommitTransactionAsync();
	}
}
