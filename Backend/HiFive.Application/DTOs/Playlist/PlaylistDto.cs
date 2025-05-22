﻿namespace HiFive.Application.DTOs.Playlist;

public class PlaylistDto
{
	public Guid Id { get; set; }

	public required string Title { get; set; }

	public Guid? ThumbnailId { get; set; }

	public Guid OwnerId { get; set; }

	public static PlaylistDto FromEntity(Domain.Models.Music.Playlist playlist)
	{
		return new PlaylistDto
		{
			Id = playlist.Id,
			Title = playlist.Title,
			ThumbnailId = playlist.ThumbnailId,
			OwnerId = playlist.OwnerId,
		};
	}

}
