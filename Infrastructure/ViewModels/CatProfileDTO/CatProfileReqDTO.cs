using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.CatProfileDTO
{
    public class CatProfileReqDTO
    {
        
        public int CatId { get; set; }
        public string HealthStatus { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string VaccinationStatus { get; set; }
        public string Description { get; set; }
    }
}
