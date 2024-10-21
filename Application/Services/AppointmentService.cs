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

        public async Task<ServiceResponse<bool>> DeleteAppointmentById(int appointmentId)
        {
            try
            {
                var response = new ServiceResponse<bool>();

                var result = await _appointmentRepo.DeleteAppointmentById(appointmentId);

                response.Data = result;
                response.Success = result;
                response.Message = "Delete status: " + result;


                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<List<AppointmentDTO>>> GetAllAppointments()
        {
            try
            {
                var response = new ServiceResponse<List<AppointmentDTO>>();

                var appointments = await _appointmentRepo.GetAllAppointments();
                var appointmentResponse = _mapper.Map<List<AppointmentDTO>>(appointments);

                response.Data = appointmentResponse;
                response.Success = true;
                response.Message = "Retrieve successfully";


                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<AppointmentDTO>> GetAppointmentById(int appointmentId)
        {
            try
            {
                var response = new ServiceResponse<AppointmentDTO>();

                var appointment = await _appointmentRepo.GetAppointmentById(appointmentId);
                var appointmentResponse = _mapper.Map<AppointmentDTO>(appointment);

                response.Data = appointmentResponse;
                response.Success = true;
                response.Message = "Retrieve successfully";


                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<AppointmentDTO>> UpdateAppointment(AppointmentDTO updatedAppointmentDto)
        {
            try
            {
                var response = new ServiceResponse<AppointmentDTO>();
                var appointment = _mapper.Map<Appointment>(updatedAppointmentDto);

                var result = await _appointmentRepo.UpdateAppointment(appointment);
                var appointmentResponse = _mapper.Map<AppointmentDTO>(result);

                response.Data = appointmentResponse;
                response.Success = true;
                response.Message = "Update successfully";


                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
