using HiFive.Application.DTOs.Misc;

namespace HiFive.Application.Contracts.Services.Contracts;

public interface IImageFileService
{
	Task<ImageDto> UploadImageAsync(ImageCreateDto imageCreateDto);
	Task<ImageDto> GetImageByIdAsync(Guid imageId);
	Task UpdateImageAsync(ImageUpdateDto imageUpdateDto);
	Task DeleteImageAsync(ImageDeleteDto imageDeleteDto);
}
