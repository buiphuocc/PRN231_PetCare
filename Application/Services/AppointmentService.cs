using Application.IService;
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.ServiceResponse;
using Infrastructure.ViewModels.AppointmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepo _appointmentRepo;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepo appointmentRepo, IMapper mapper)
        {
            _appointmentRepo = appointmentRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AppointmentDTO>> AddAppointment(AppointmentDTO appointmentDto)
        {
            try
            {
                var response = new ServiceResponse<AppointmentDTO>();

                var appointment = _mapper.Map<Appointment>(appointmentDto);
                var appointmentResponse =  await _appointmentRepo.AddAppointment(appointment);
                var responseData = _mapper.Map<AppointmentDTO>(appointmentResponse);

                response.Message = "Add successfully";
                response.Success = true;
                response.Data = responseData;

                return response;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
