﻿using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Playlist;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class PlaylistController : ControllerBase
{
	private readonly IPlaylistService _playlistService;
	public PlaylistController(IPlaylistService playlistService)
	{
		_playlistService = playlistService;
	}

	[HttpPost]
	public async Task<IActionResult> Create(PlaylistCreateDto playlistCreateDto)
	{
		return Ok(await _playlistService.CreatePlaylistAsync(playlistCreateDto));
	}

	[HttpPost("add/{playlistid}-{songid}")]
	public async Task<IActionResult> AddSongToPlaylist(Guid playlistid, Guid songid)
	{
		await _playlistService.AddSongToPlaylistAsync(playlistid, songid);
		return NoContent();
	}

	[HttpGet("id/{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		return Ok(await _playlistService.GetPlaylistByIdAsync(id));
	}

	[HttpGet("details-id/{id}")]
	public async Task<IActionResult> GeDetailstById(Guid id)
	{
		return Ok(await _playlistService.GetPlaylistDetailsByIdAsync(id));
	}

	[HttpGet("user/{id}")]
	public async Task<IActionResult> GetPlaylistsByUserId(Guid id)
	{
		return Ok(await _playlistService.GetPlaylistsByUserIdAsync(id));
	}

	[HttpPut]
	public async Task<IActionResult> Update(PlaylistUpdateDto playlist)
	{
		await _playlistService.UpdatePlaylistAsync(playlist);
		return NoContent();
	}
}
