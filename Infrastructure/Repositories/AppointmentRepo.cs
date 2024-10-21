using Domain.Entities;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly PetCareDbContext _dbContext;

        public AppointmentRepo(PetCareDbContext context)
        {
            _dbContext = context;
        }
        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            try
            {
                await _dbContext.Appointments.AddAsync(appointment);
                await _dbContext.SaveChangesAsync();
                return appointment;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            try
            {
                return await _dbContext.Appointments.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Appointment> GetAppointmentById(int appointmentId)
        {
            try
            {
                return await _dbContext.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Appointment> UpdateAppointment(Appointment updatedAppointment)
        {
            try
            {
                var appointment =  await _dbContext.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == updatedAppointment.AppointmentId);
                if (appointment == null)
                {
                    throw new Exception("Appointment not found");
                }

                appointment.AppointmentDate = updatedAppointment.AppointmentDate;
                appointment.Purpose = updatedAppointment.Purpose;
                appointment.CatId = updatedAppointment.CatId;
                appointment.UserId = updatedAppointment.UserId;

                _dbContext.Appointments.Update(appointment);
                await _dbContext.SaveChangesAsync();

                return appointment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAppointmentById(int appointmentId)
        {
            try
            {
                var appointment = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

                if (appointment == null)
                {
                    throw new Exception("Appointment not found");
                }

                _dbContext.Appointments.Remove(appointment);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting appointment: {ex.Message}");
            }
        }
    }
}
