using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Album;
using HiFive.Domain.Models.Music;
using HiFive.Presentation.Controllers.Requests.Album;
using HiFive.Presentation.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AlbumController : ControllerBase
{
	private readonly IAlbumService _albumService;
	private readonly ISongService _songService;
	private readonly IImageFileService _imageFileService;
	private readonly BlobService _blobService;

	public AlbumController(IAlbumService albumService, ISongService songService, IImageFileService imageFileService, BlobService blobService)
	{
		_albumService = albumService;
		_songService = songService;
		_imageFileService = imageFileService;
		_blobService = blobService;
	}

	[HttpPost]
	[Authorize(Policy = "VerifiedDistributorOnly")]
	public async Task<IActionResult> Create([FromForm] AlbumCreateRequest album)
	{
		Guid? imageId = null;
		if (album.CoverImage != null)
			imageId = (await _imageFileService.UploadImageAsync(ImageDtoHelper.CreateDtoFromFormFile(album.CoverImage))).Id;
		
		var albumCreateDto = album.ToAlbumCreateDto(imageId);
		var albumDto = await _albumService.CreateAlbumAsync(albumCreateDto);
		int order = 1;
		foreach (var song in album.Songs)
		{
			var imageCreateDto = ImageDtoHelper.CreateDtoFromFormFile(song.CoverImage);
			var imageDto = await _imageFileService.UploadImageAsync(imageCreateDto);
			var uploadedSong = await _blobService.UploadFileAsync(song.Data);
			var songDto = song.ToSongCreateDto(imageDto.Id, uploadedSong.Uri);
			songDto.Duration = uploadedSong.Duration;
			songDto.AlbumId = albumDto.Id;
			songDto.OrderIndex = order++;
			await _songService.CreateSongAsync(songDto);
		}
		return Ok(albumDto);
	}
	
	[HttpGet("id/{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		return Ok(await _albumService.GetAlbumByIdAsync(id));
	}

	[HttpGet("details/{id}")]
	public async Task<IActionResult> GetDetailsById(Guid id)
	{
		return Ok(await _albumService.GetAlbumDetailsByIdAsync(id));
	}

	[HttpGet("artist/{id}")]
	public async Task<IActionResult> GetByArtistId(Guid id)
	{
		return Ok(await _albumService.GetAllAlbumsByArtistAsync(id));
	}

	[HttpGet("name/{partialName}")]
	public async Task<IActionResult> GetByPartialName(string partialName)
	{
		return Ok(await _albumService.GetAllAlbumsByPartialTitleAsync(partialName));
	}

	[HttpPut]
	[Authorize(Policy = "VerifiedDistributorOnly")]
	public async Task<IActionResult> Update(AlbumUpdateDto album)
	{
		await _albumService.UpdateAlbumAsync(album);
		return NoContent();
	}
}
