using Application;
using Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PetCareDbContext _dbContext;
        private readonly IUserRepo _userRepository;
        public UnitOfWork(PetCareDbContext dbContext, IUserRepo userRepo)
        {
            _dbContext = dbContext;
            _userRepository = userRepo;
        }
        public IUserRepo UserRepository => _userRepository;
        public  Task<int> SaveChangeAsync()
        {
            return  _dbContext.SaveChangesAsync();
        }       
    }
}
