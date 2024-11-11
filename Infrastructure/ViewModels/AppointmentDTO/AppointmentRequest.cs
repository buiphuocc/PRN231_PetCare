using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.AppointmentDTO
{
    public class AppointmentRequest
    {
        public int CatId { get; set; }
        public int UserId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Purpose { get; set; }
    }
}
