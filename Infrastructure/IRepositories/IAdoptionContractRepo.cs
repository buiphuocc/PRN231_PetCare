using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IAdoptionContractRepo : IGenericRepo<AdoptionContract>
    {
        Task<List<AdoptionContract>> GetAllAdoptionContracts();
        Task<AdoptionContract> GetAdoptionContractById(int id);
        Task<AdoptionContract> GetAdoptionContractByApplicationId(int id);
    }
}
