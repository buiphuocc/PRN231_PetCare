using Domain.Entities;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class CatRepo : GenericRepo<Cat>, ICatRepo
{

    private readonly PetCareDbContext _context;
    public CatRepo(PetCareDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Cat>> GetAllCats(int pageNumber, int pageSize)
    {
        return await _context.Cats
            .Skip((pageNumber - 1) * pageSize) // Skip records for previous pages
            .Take(pageSize) // Take only the number of records for the current page
            .ToListAsync();
    }

    public async Task<Cat?> GetCatById(int id)
    {
        return await _context.Cats.Where(m => m.CatId == id).FirstOrDefaultAsync();
    }

    public async Task<Cat?> GetCatByName(string catName)
    {
        return await _context.Cats.Where(m => m.Name == catName).FirstOrDefaultAsync();
    }

}
