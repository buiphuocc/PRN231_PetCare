using Domain.Entities;
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
        Task<ServiceResponse<AppointmentResponse>> AddAppointment(AppointmentResponse appointmentDto);
        Task<ServiceResponse<List<AppointmentResponse>>> GetAllAppointments();
        Task<ServiceResponse<AppointmentResponse>> GetAppointmentById(int appointmentId);
        Task<ServiceResponse<AppointmentResponse>> UpdateAppointment(AppointmentResponse updatedAppointmentDto, int appointmentId);
        Task<ServiceResponse<bool>> DeleteAppointmentById(int appointmentId);

        Task<ServiceResponse<List<AppointHistory>>> GetAllAppointmentsByAdopterId(int adopterId);
    }
}
