using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AdoptionApplication
    {
        public int ApplicationId { get; set; }
        public int CatId { get; set; }
        public int AdopterId { get; set; }
        public int AdoptionFee { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string ApplicationStatus { get; set; } // Pending, Approved, Rejected
        public DateTime? AdoptionDate { get; set; }

        // Navigation properties
        public Cat Cat { get; set; }
        public Account Adopter { get; set; }
        public AdoptionContract Contract { get; set; }
    }

}
