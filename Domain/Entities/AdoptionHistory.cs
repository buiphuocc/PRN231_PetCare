using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AdoptionHistory
    {
        public int AdoptionId { get; set; }
        public int CatId { get; set; }
        public int AdopterId { get; set; }
        public DateTime AdoptionDate { get; set; }

        // Navigation properties
        public Cat Cat { get; set; }
        public Account Adopter { get; set; }
    }

}
