using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EventParticipation
    {
        public int ParticipationId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }  // Volunteer, Organizer

        // Navigation properties
        public Event Event { get; set; }
        public Account User { get; set; }
    }

}
