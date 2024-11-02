using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.DonationDTO
{
    public class DonationResDTO
    {
        public int DonationId { get; set; }
        public int DonorId { get; set; }

        public string DonorName { get; set; }
        public int ShelterId { get; set; }

        public string ShelterName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
