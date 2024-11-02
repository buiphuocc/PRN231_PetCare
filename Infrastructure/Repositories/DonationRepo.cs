using Domain.Entities;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DonationRepo : GenericRepo<Donation>, IDonationRepo
    {
        private readonly PetCareDbContext _context;


        public DonationRepo(PetCareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Donation>> GetAllDonation(int pageNumber, int pageSize)
        {
            return await _context.Donations
                .Skip((pageNumber - 1) * pageSize) // Skip records for previous pages
                .Take(pageSize) // Take only the number of records for the current page
                .ToListAsync();
        }

        public async Task<Donation?> GetDonationByDonationId(int id)
        {
            return await _context.Donations.Where(m => m.DonationId == id).FirstOrDefaultAsync();
        }

        public async Task<Donation?> GetDonationByDonationShielterId(int id)
        {
            return await _context.Donations.Where(m => m.ShelterId == id).FirstOrDefaultAsync();
        }

        public async Task<Donation?> GetDonationByDonorId(int id)
        {
            return await _context.Donations.Where(m => m.DonorId == id).FirstOrDefaultAsync();
        }

        public async Task<List<Donation>> GetDonorsByDonationIdAsync(int donationId)
        {
            return await _context.Donations
                .Where(d => _context.Donations.Any(dn => dn.DonationId == donationId && dn.DonorId == d.DonorId))
                .ToListAsync();
        }

        public async Task<List<Donation>> GetDonationsByDonorIdAsync(int donorId)
        {
            return await _context.Donations
                .Where(d => d.DonorId == donorId)
                .ToListAsync();
        }
    }
}
