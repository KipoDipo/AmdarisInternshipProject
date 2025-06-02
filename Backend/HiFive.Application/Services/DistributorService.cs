using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Distributor;
using HiFive.Application.UnitOfWork;

namespace HiFive.Application.Services;
public class DistributorService : IDistributorService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IValidator _validator;

	public DistributorService(IUnitOfWork unitOfWork, IValidator validator)
	{
		_unitOfWork = unitOfWork;
		_validator = validator;
	}

	public async Task<DistributorDto> CreateDistributorAsync(DistributorCreateDto distributorCreateDto)
	{
		var distributor = await _unitOfWork.Distributors.Register(distributorCreateDto);
		return DistributorDto.FromEntity(distributor);
	}

	public async Task AddArtistToDistributor(Guid distributorId, Guid artistId)
	{
		var distributor = await _unitOfWork.Distributors.GetWithDetailsByIdAsync(distributorId);
		_validator.Validate(distributor);

		var artist = await _unitOfWork.Artists.GetByIdAsync(artistId);
		_validator.Validate(artist);

		await _unitOfWork.BeginTransactionAsync();
		distributor.Artists.Add(artist);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task<DistributorDto> GetDistributorByIdAsync(Guid distributorId)
	{
		var distributor = await _unitOfWork.Distributors.GetByIdAsync(distributorId);
		_validator.Validate(distributor);

		return DistributorDto.FromEntity(distributor);
	}

	public async Task<DistributorDetailsDto> GetDistributorDetailsByIdAsync(Guid distributorId)
	{
		var distributor = await _unitOfWork.Distributors.GetWithDetailsByIdAsync(distributorId);
		_validator.Validate(distributor);

		return DistributorDetailsDto.FromEntity(distributor);
	}

	public async Task<IEnumerable<DistributorDto>> GetDistributorsByPartialNameAsync(string partialName)
	{
		var distributors = await _unitOfWork.Distributors.GetAllByPartialName(partialName);

		return distributors.Select(DistributorDto.FromEntity);
	}

	public async Task UpdateDistributorAsync(DistributorUpdateDto artistUpdateDto)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteDistributorAsync(Guid distributorId)
	{
		throw new NotImplementedException();
	}
}
