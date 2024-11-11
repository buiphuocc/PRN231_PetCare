using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.AdoptionApplicationDTO
{
    public class AdoptionHistoryRes
    {
        public int ApplicationId { get; set; }
        public int AdopterId { get; set; }
        public int AdoptionFee { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string ApplicationStatus { get; set; } // Pending, Approved, Rejected
        public DateTime? AdoptionDate { get; set; }
        public string CatName { get; set; } // Replaces CatId with CatName
    }
}
