﻿using Domain.Entities;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AdoptionContractRepo : GenericRepo<AdoptionContract>, IAdoptionContractRepo
    {
        private readonly PetCareDbContext _context;
        public AdoptionContractRepo(PetCareDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<AdoptionContract>> GetAllAdoptionContracts(int pageNumber, int pageSize)
        {
            return await _context.AdoptionContracts.Skip((pageNumber - 1) * pageSize) // Skip records for previous pages
                .Take(pageSize) // Take only the number of records for the current page
                .ToListAsync();
        }

        public async Task<AdoptionContract> GetAdoptionContractById(int id)
        {
            return await _context.AdoptionContracts.Where(m => m.ContractId == id).FirstOrDefaultAsync();
        }

        public async Task<AdoptionContract> GetAdoptionContractByApplicationId(int applicationid)
        {
            return await _context.AdoptionContracts.Where(m => m.ApplicationId == applicationid).FirstOrDefaultAsync();
        }
    }
}
