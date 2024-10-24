using Domain.Entities;
using Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ShelterRepo : GenericRepo<Shelter>, IShelterRepo
    {
        private readonly PetCareDbContext _context;
        public ShelterRepo(PetCareDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
