using Infrastructure.ServiceResponse;
using Infrastructure.ViewModels.AppointmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IAppointmentService
    {
        Task<ServiceResponse<AppointmentDTO>> AddAppointment(AppointmentDTO appointmentDto);
    }
}
