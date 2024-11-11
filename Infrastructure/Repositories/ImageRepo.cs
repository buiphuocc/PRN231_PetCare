﻿using Domain.Entities;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Infrastructure.Repositories
{
    public class ImageRepo : GenericRepo<EntityImage>, IImageRepo
    {
        private readonly PetCareDbContext _dbContext;

        public ImageRepo(PetCareDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<EntityImage> GetImageInforById(int entityId, string entityType)
        {
            return await _dbContext.EntityImages
                .FirstOrDefaultAsync(e => e.EntityId == entityId && e.EntityType == entityType);
        }


        public async Task<IEnumerable<EntityImage>> GetAllImageInfors()
        {
            return _dbContext.EntityImages.ToList();
        }
        public async Task DeleteImage(int id)
        {
            var iproduct = await _dbContext.EntityImages.FindAsync(id);
            if (iproduct != null)
            {
                _dbContext.EntityImages.Remove(iproduct);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<EntityImage> AddImage(EntityImage entityImage)
        {
            _dbContext.EntityImages.Add(entityImage);
            await _dbContext.SaveChangesAsync();
            return entityImage;  
        }

    }
}
