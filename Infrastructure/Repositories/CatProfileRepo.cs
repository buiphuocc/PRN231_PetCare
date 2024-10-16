using Domain.Entities;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CatProfileRepo : GenericRepo<CatProfile>, ICatProfileRepo
    {
        private readonly PetCareDbContext _context;
        public CatProfileRepo(PetCareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CatProfile>> GetAllCatPs(int pageNumber, int pageSize)
        {
            return await _context.CatProfiles
                .Skip((pageNumber - 1) * pageSize) // Skip records for previous pages
                .Take(pageSize) // Take only the number of records for the current page
                .ToListAsync();
        }

        public async Task<CatProfile?> GetCatPById(int id)
        {
            return await _context.CatProfiles.Where(m => m.ProfileId == id).FirstOrDefaultAsync();
        }

        public async Task<CatProfile?> GetCatPByCatId(int catId)
        {
            return await _context.CatProfiles.Where(m => m.CatId == catId).FirstOrDefaultAsync();
        }
    }
}
