using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Purpose { get; set; }
        public int ShelterId { get; set; }
        public decimal TotalFundsRaised { get; set; }

        // Navigation property
        public Shelter Shelter { get; set; }
        public ICollection<EventParticipation> Participations { get; set; }
    }

}
