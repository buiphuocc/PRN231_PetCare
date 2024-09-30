﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int CatId { get; set; }
        public int UserId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Purpose { get; set; }  // Adoption, Health Checkup, Vaccination

        // Navigation properties
        public Cat Cat { get; set; }
        public Account User { get; set; }
    }

}
