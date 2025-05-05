using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Application.DTOs.Misc;
using HiFive.Application.UnitOfWork;
using HiFive.Domain.Models.Misc;

namespace HiFive.Application.Services;

public class ImageFileService : IImageFileService
{
	private IUnitOfWork _unitOfWork;
	private IValidator _validator;

	public ImageFileService(IUnitOfWork unitOfWork, IValidator validator)
	{
		_unitOfWork = unitOfWork;
		_validator = validator;
	}

	public async Task<ImageDto> UploadImageAsync(ImageCreateDto imageCreateDto)
	{
		await _unitOfWork.BeginTransactionAsync();

		ImageFile imageFile = new ImageFile()
		{
			Data = imageCreateDto.Data,
			ContentType = imageCreateDto.ContentType
		};

		await _unitOfWork.Images.AddAsync(imageFile);

		await _unitOfWork.CommitTransactionAsync();

		return ImageDto.FromEntity(imageFile);
	}

	public async Task<ImageDto> GetImageByIdAsync(Guid imageId)
	{
		var image = await _unitOfWork.Images.GetByIdAsync(imageId);
		_validator.Validate(image);

		return ImageDto.FromEntity(image);
	}

	public async Task UpdateImageAsync(ImageUpdateDto imageUpdateDto)
	{
		await _unitOfWork.BeginTransactionAsync();
		var image = await _unitOfWork.Images.GetByIdAsync(imageUpdateDto.Id);
		_validator.Validate(image);

		image.Data = imageUpdateDto.Data ?? image.Data;
		image.ContentType = imageUpdateDto.ContentType ?? image.ContentType;

		await _unitOfWork.Images.UpdateAsync(image);
		await _unitOfWork.CommitTransactionAsync();
	}

	public async Task DeleteImageAsync(ImageDeleteDto imageDeleteDto)
	{
		await _unitOfWork.BeginTransactionAsync();

		await _unitOfWork.Images.DeleteAsync(imageDeleteDto.Id);

		await _unitOfWork.CommitTransactionAsync();
	}
}
