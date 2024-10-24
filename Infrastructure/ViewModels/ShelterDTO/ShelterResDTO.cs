using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.ShelterDTO
{
    public class ShelterResDTO
    {
        public int ShelterId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string ContactInfo { get; set; }
        public int? ManagerId { get; set; }
    }
}
