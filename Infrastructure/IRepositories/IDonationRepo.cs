using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IDonationRepo : IGenericRepo<Donation>
    {
        Task<List<Donation>> GetAllDonation(int pageNumber, int pageSize);
        Task<Donation> GetDonationByDonationId(int id);

        Task<Donation> GetDonationByDonationShielterId(int id);
        Task<Donation> GetDonationByDonorId(int id);

        Task<List<Donation>> GetDonorsByDonationIdAsync(int donationId);

        Task<List<Donation>> GetDonationsByDonorIdAsync(int donorId);
    }
}
