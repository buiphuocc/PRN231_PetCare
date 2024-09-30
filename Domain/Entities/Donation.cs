using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Donation
    {
        public int DonationId { get; set; }
        public int DonorId { get; set; }
        public int ShelterId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        // Navigation properties
        public Account Donor { get; set; }
        public Shelter Shelter { get; set; }
    }

}
