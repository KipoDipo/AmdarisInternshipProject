using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Playlist;
using HiFive.Presentation.Controllers.Requests.Playlist;
using HiFive.Presentation.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PlaylistController : ControllerBase
{
	private readonly IPlaylistService _playlistService;
	private readonly ICurrentUserService _currentUserService;
	private readonly IImageFileService _imageFileService;

	public PlaylistController(IPlaylistService playlistService, ICurrentUserService currentUserService, IImageFileService imageFileService)
	{
		_playlistService = playlistService;
		_currentUserService = currentUserService;
		_imageFileService = imageFileService;
	}

	// Could be used so that an admin/the app can make a playlist for you
	[HttpPost]
	public async Task<IActionResult> Create(PlaylistCreateDto playlistCreateDto)
	{
		return Ok(await _playlistService.CreatePlaylistAsync(playlistCreateDto));
	}

	[HttpPost("create-my-playlist")]
	public async Task<IActionResult> CreateMy([FromForm] PlaylistCreateMyRequest request)
	{
		if (request.Thumbnail == null)
		{
			var dto = request.ToPlaylistCreateMyDto(_currentUserService.Id, null);
			return await Create(dto);
		}
		else
		{
			var imageDto = await _imageFileService.UploadImageAsync(ImageDtoHelper.CreateDtoFromFormFile(request.Thumbnail));
			var dto = request.ToPlaylistCreateMyDto(_currentUserService.Id, imageDto.Id);
			return await Create(dto);
		}
	}

	[HttpPost("add/{playlistid}-{songid}")]
	public async Task<IActionResult> AddSongToPlaylist(Guid playlistId, Guid songid)
	{
		// check if user owns the playlist
		var playlist = await _playlistService.GetPlaylistByIdAsync(playlistId);
		if (playlist != null && playlist.OwnerId != _currentUserService.Id)
			return Unauthorized();

		await _playlistService.AddSongToPlaylistAsync(playlistId, songid);
		return NoContent();
	}

	[HttpGet("id/{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		return Ok(await _playlistService.GetPlaylistByIdAsync(id));
	}

	[HttpGet("details/{id}")]
	public async Task<IActionResult> GetDetailstById(Guid id)
	{
		return Ok(await _playlistService.GetPlaylistDetailsByIdAsync(id));
	}

	[HttpGet("user/{id}")]
	public async Task<IActionResult> GetPlaylistsByUserId(Guid id)
	{
		return Ok(await _playlistService.GetPlaylistsByUserIdAsync(id));
	}

	[HttpGet("my-playlists")]
	public async Task<IActionResult> GetMyPlaylists()
	{
		return await GetPlaylistsByUserId(_currentUserService.Id);
	}

	[HttpPut]
	public async Task<IActionResult> Update(PlaylistUpdateDto playlist)
	{
		await _playlistService.UpdatePlaylistAsync(playlist);
		return NoContent();
	}
}
