using Domain.Entities;
using Infrastructure.ServiceResponse;
using Infrastructure.ViewModels.AdoptionContractDTO;
using Infrastructure.ViewModels.CatProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IAdoptionContractService
    {
        Task<ServiceResponse<List<AdoptionContractRes>>> GetAllAdoptionContractsAsync();
        Task<ServiceResponse<AdoptionContractRes>> GetAdoptionContractByIdAsync(int id);
        Task<ServiceResponse<AdoptionContractRes>> CreateAdoptionContract(AdoptionContractReq adoptionContract);
        Task<ServiceResponse<bool>> Delete(int id);
        Task<ServiceResponse<AdoptionContractRes>> UpdateAdoptionContract(AdoptionContractReq adoptionContract, int id);
    }
}
