using Domain.Entities;
using Infrastructure.IRepositories;
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
    }
}
