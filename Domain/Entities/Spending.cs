using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Spending
    {
        public int SpendingId { get; set; }
        public int ShelterId { get; set; }
        public decimal Amount { get; set; }
        public DateTime SpendingDate { get; set; }
        public string Description { get; set; }

        // Navigation properties
        public Shelter Shelter { get; set; }
    }

}
