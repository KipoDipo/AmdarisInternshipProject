﻿using HiFive.Application.DTOs.Listener;

namespace HiFive.Application.Contracts.Services.Contracts;

public interface IListenerService
{
	Task<ListenerDto> CreateListenerAsync(ListenerCreateDto listenerCreateDto);

	Task<ListenerDto> GetListenerByIdAsync(Guid listenerId);
	Task<IEnumerable<ListenerDto>> GetListenersByPartialNameAsync(string partialName);
	Task<ListenerDetailsDto> GetListenerDetailsByIdAsync(Guid listenerId);

	Task UpdateListenerAsync(ListenerUpdateDto listenerUpdateDto);
}
