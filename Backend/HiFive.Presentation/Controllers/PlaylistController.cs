using HiFive.Application.AwardSystem;
using HiFive.Application.AwardSystem.BadgeStrategies;
using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Playlist;
using HiFive.Presentation.Controllers.Requests.Playlist;
using HiFive.Presentation.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Authorize(Roles = "Listener")]
[Route("[controller]")]
public class PlaylistController : ControllerBase
{
	private readonly IPlaylistService _playlistService;
	private readonly ICurrentUserService _currentUserService;
	private readonly IImageFileService _imageFileService;
	private readonly Awarder _awarder;

	public PlaylistController(IPlaylistService playlistService, ICurrentUserService currentUserService, IImageFileService imageFileService, Awarder awarder)
	{
		_playlistService = playlistService;
		_currentUserService = currentUserService;
		_imageFileService = imageFileService;
		_awarder = awarder;
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
		PlaylistCreateDto dto;
		if (request.Thumbnail == null)
		{
			dto = request.ToPlaylistCreateMyDto(_currentUserService.Id, null);
		}
		else
		{
			var imageDto = await _imageFileService.UploadImageAsync(ImageDtoHelper.CreateDtoFromFormFile(request.Thumbnail));
			dto = request.ToPlaylistCreateMyDto(_currentUserService.Id, imageDto.Id);
		}

		var result = await Create(dto);

		var playlists = await _playlistService.GetPlaylistsByUserIdAsync(_currentUserService.Id);
		if (playlists.Count() == 3)
			await _awarder.Award(_currentUserService.Id, new Created3Playlists());

		return result;
	}

	[HttpPost("add/{playlistId}/song/{songid}")]
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
