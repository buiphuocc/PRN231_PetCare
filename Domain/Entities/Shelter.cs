using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Shelter
    {
        public int ShelterId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string ContactInfo { get; set; }
        public int? ManagerId { get; set; }

        // Navigation properties
        public Account Manager { get; set; }
        public ICollection<Account> Volunteers { get; set; }
        public ICollection<VolunteerActivity> VolunteerActivities { get; set; }
        public ICollection<Cat> Cats { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Donation> Donations { get; set; }
        public ICollection<Spending> Spendings { get; set; }
    }

}
