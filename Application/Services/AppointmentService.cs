using Application.IService;
using AutoMapper;
using CloudinaryDotNet;
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
        private readonly IEmailService _emailService;
        private readonly ICatRepo _catRepo;

        public AppointmentService(IAppointmentRepo appointmentRepo, IMapper mapper, IEmailService emailService, ICatRepo catRepo)
        {
            _appointmentRepo = appointmentRepo;
            _mapper = mapper;
            _emailService = emailService;
            _catRepo = catRepo;
        }

        public async Task<ServiceResponse<AppointmentResponse>> AddAppointment(AppointmentResponse appointmentDto)
        {
            try
            {
                var response = new ServiceResponse<AppointmentResponse>();

                var appointment = _mapper.Map<Appointment>(appointmentDto);
                await _appointmentRepo.AddAsync(appointment);
                var appointmentCheck = await _appointmentRepo.GetByIdAsync(appointment.AppointmentId);
                if(appointmentCheck != null)
                {
                    var responseData = _mapper.Map<AppointmentResponse>(appointmentCheck);

                    response.Message = "Add successfully";
                    response.Success = true;
                    response.Data = responseData;

                    // Prepare email content
                    var subject = "Appointment Confirmation";
                    var body = $"Hello, <br/>Your appointment for '{appointmentDto.Purpose}' has been scheduled on {appointmentDto.AppointmentDate}. Thank you!";

                    // Send email notification
                    await _emailService.SendEmailAsync(appointmentCheck.User.Email, subject, body);

                    response.Message = "Add successfully";
                    response.Success = true;
                    response.Data = responseData;
                }
                

                return response;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> DeleteAppointmentById( int id)
        {
            try
            {
                var response = new ServiceResponse<bool>();


                var appointmentCheck = await _appointmentRepo.GetByIdAsync(id);

                if(appointmentCheck != null)
                {
                    await _appointmentRepo.Remove(appointmentCheck);
                    response.Success = true;
                    response.Message = "Delete status: " + response.Success;
                }

                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<List<AppointmentResponse>>> GetAllAppointments()
        {
            try
            {
                var response = new ServiceResponse<List<AppointmentResponse>>();

                var appointments = await _appointmentRepo.GetAllAsync();
                var appointmentResponse = _mapper.Map<List<AppointmentResponse>>(appointments);

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

        public async Task<ServiceResponse<AppointmentResponse>> GetAppointmentById(int appointmentId)
        {
            try
            {
                var response = new ServiceResponse<AppointmentResponse>();

                var appointment = await _appointmentRepo.GetByIdAsync(appointmentId);
                var appointmentResponse = _mapper.Map<AppointmentResponse>(appointment);

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

        public async Task<ServiceResponse<AppointmentResponse>> UpdateAppointment(AppointmentResponse updatedAppointmentDto, int id)
        {
            try
            {
                var response = new ServiceResponse<AppointmentResponse>();
                var appointment = _mapper.Map<Appointment>(updatedAppointmentDto);
                var appointmentCheck = await _appointmentRepo.GetByIdAsync(id);
                if(appointmentCheck != null)
                {
                    await _appointmentRepo.Update(appointment);
                    var appointmentResponse = _mapper.Map<AppointmentResponse>(appointmentCheck);

                    response.Data = appointmentResponse;
                    response.Success = true;
                    response.Message = "Update successfully";

                }

                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ServiceResponse<List<AppointHistory>>> GetAllAppointmentsByAdopterId(int adopterId)
        {
            var response = new ServiceResponse<List<AppointHistory>>();

            try
            {
                var appointments = await _appointmentRepo.GetAllAsync();

                // Filter appointments by adopter ID
                var filteredAppointments = appointments.Where(a => a.UserId == adopterId).ToList();

                // Create a list to hold the enriched response
                var appointmentResponses = new List<AppointHistory>();

                foreach (var appointment in filteredAppointments)
                {
                    var cat = await _catRepo.GetByIdAsync(appointment.CatId);
                    var catName = cat != null ? cat.Name : "Unknown";

                    // Map to the new DTO
                    appointmentResponses.Add(new AppointHistory
                    {
                        AppointmentId = appointment.AppointmentId,
                        CatName = catName,
                        UserId = appointment.UserId,
                        AppointmentDate = appointment.AppointmentDate,
                        Purpose = appointment.Purpose
                    });
                }

                response.Data = appointmentResponses;
                response.Success = true;
                response.Message = "Retrieve successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to retrieve appointments: {ex.Message}";
            }

            return response;
        }
    }
}
