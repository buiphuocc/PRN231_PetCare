using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VolunteerActivity
    {
        public int ActivityId { get; set; }
        public int UserId { get; set; }
        public int ShelterId { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityDescription { get; set; }
        public int HoursWorked { get; set; }

        // Navigation properties
        public Account User { get; set; }
        public Shelter Shelter { get; set; }
    }

}
