using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Listener;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ListenerController : ControllerBase
{
	private readonly IListenerService _listenerService;
	public ListenerController(IListenerService listenerService)
	{
		_listenerService = listenerService;
	}

	[HttpPost]
	public async Task<IActionResult> Create(ListenerCreateDto listener)
	{
		return Ok(await _listenerService.CreateListenerAsync(listener));
	}

	[HttpGet("id/{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		return Ok(await _listenerService.GetListenerByIdAsync(id));
	}

	[HttpGet("name/{partialName}")]
	public async Task<IActionResult> GetByPartialName(string partialName)
	{
		return Ok(await _listenerService.GetListenersByPartialNameAsync(partialName));
	}

	[HttpGet("details/{id}")]
	public async Task<IActionResult> GetDetailsById(Guid id)
	{
		return Ok(await _listenerService.GetListenerDetailsByIdAsync(id));
	}

	[HttpPut]
	public async Task<IActionResult> Update(ListenerUpdateDto listener)
	{
		await _listenerService.UpdateListenerAsync(listener);
		return NoContent();
	}
}
