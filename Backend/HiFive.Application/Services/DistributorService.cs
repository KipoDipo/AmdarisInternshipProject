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

	public Task DeleteDistributorAsync(Guid artistId)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<DistributorDto>> GetAllDistributorsAsync()
	{
		throw new NotImplementedException();
	}

	public Task<DistributorDto> GetDistributorByIdAsync(Guid artistId)
	{
		throw new NotImplementedException();
	}

	public Task<DistributorDetailsDto> GetDistributorDetailsByIdAsync(Guid artistId)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<DistributorDto>> GetDistributorsByPartialNameAsync(string partialName)
	{
		throw new NotImplementedException();
	}

	public Task UpdateDistributorAsync(DistributorUpdateDto artistUpdateDto)
	{
		throw new NotImplementedException();
	}
}
