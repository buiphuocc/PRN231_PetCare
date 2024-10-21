using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IAppointmentRepo
    {
        Task<Appointment> AddAppointment(Appointment appointment);
    }
}
