using HiFive.Application.DTOs.Distributor;

namespace HiFive.Application.Contracts.Services.Contracts;

public interface IDistributorService
{
	Task<DistributorDto> CreateDistributorAsync(DistributorCreateDto distributorCreateDto);

	Task<IEnumerable<DistributorDto>> GetAllDistributorsAsync();
	Task<DistributorDto> GetDistributorByIdAsync(Guid distributorId);
	Task<IEnumerable<DistributorDto>> GetDistributorsByPartialNameAsync(string partialName);
	Task<DistributorDetailsDto> GetDistributorDetailsByIdAsync(Guid distributorId);

	Task UpdateDistributorAsync(DistributorUpdateDto distributorUpdateDto);

	Task DeleteDistributorAsync(Guid distributorId);
}

