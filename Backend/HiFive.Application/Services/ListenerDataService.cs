using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Song;
using HiFive.Application.UnitOfWork;
using HiFive.Domain.Models.Statistics;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Application.Services;

public class ListenerDataService : IListenerDataService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IValidator _validator;

	public ListenerDataService(IUnitOfWork unitOfWork, IValidator validator)
	{
		_unitOfWork = unitOfWork;
		_validator = validator;
	}

	public async Task AddListenedSong(Guid listenerId, Guid songId)
	{
		var listener = await _unitOfWork.Listeners.GetByIdAsync(listenerId);
		_validator.Validate(listener);

		var song = await _unitOfWork.Songs.GetByIdAsync(songId);
		_validator.Validate(song);

		var data = new ListenerData()
		{
			ListenerId = listenerId,
			SongId = songId,
			ListenedOn = DateTime.Now
		};

		await _unitOfWork.BeginTransactionAsync();
		await _unitOfWork.ListenerData.AddAsync(data);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task<IEnumerable<SongDto>> GetUniqueSongsListenedById(Guid listenerId, int count = 8, int forTheLastNMonths = 1)
	{
		var listener = await _unitOfWork.Listeners.GetByIdAsync(listenerId);
		_validator.Validate(listener);

		var afterDate = DateTime.Now.AddMonths(-forTheLastNMonths);

		var songs = _unitOfWork.ListenerData.GetAllNoTracking()
			.Include(ld => ld.Song)
				.ThenInclude(s => s.Artist)
			.Where(ld =>
				ld.ListenerId == listenerId &&
				ld.ListenedOn >= afterDate)
			.OrderByDescending(ld => ld.ListenedOn)
			.Select(ld => ld.Song)
			.AsEnumerable()
			.DistinctBy(x => x.Id)
			.Take(count);

		return songs.Select(SongDto.FromEntity);
	} 
}
