using HiFive.Application.Contracts.Repositories;
using HiFive.Domain.Models.Misc;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public class ImageFileRepository : BaseRepository<ImageFile>, IImageFileRepository
{
	public ImageFileRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
