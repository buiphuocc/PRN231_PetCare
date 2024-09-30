using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cat
    {
        public int CatId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Breed { get; set; }
        public bool IsAdopted { get; set; }
        public int ShelterId { get; set; }

        // Navigation property
        public Shelter Shelter { get; set; }
        public ICollection<AdoptionHistory> AdoptionHistories { get; set; }
        public ICollection<CatProfile> CatProfiles { get; set; }
        public ICollection<AdoptionApplication> AdoptionApplications { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }

}
