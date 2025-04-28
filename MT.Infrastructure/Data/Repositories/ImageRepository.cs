using Microsoft.EntityFrameworkCore;
using MT.Domain.Entities;
using MT.Domain.Interfaces;

namespace MT.Infrastructure.Data.Repositories;

public class ImageRepository(ApplicationContext context) : IImageRepository
{
    public async Task<ItemImageEntity> GetImageByIdAsync(long imageId)
    {
        return await context.ItemImages.FindAsync(imageId);
    }

    public async Task<ItemImageEntity> PostImageAsync(ItemImageEntity imageEntity)
    {
        await context.ItemImages.AddAsync(imageEntity);
        await context.SaveChangesAsync();
        return imageEntity;
    }
}