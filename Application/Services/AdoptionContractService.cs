using Application.IService;
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.ServiceResponse;
using Infrastructure.ViewModels.AdoptionContractDTO;
using Infrastructure.ViewModels.CatProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdoptionContractService : IAdoptionContractService
    {
        private readonly IAdoptionContractRepo _repo;
        private readonly IMapper _mapper;

        public AdoptionContractService(IMapper mapper,IAdoptionContractRepo repo)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<AdoptionContractRes>>> GetAllAdoptionContractsAsync(int pageNumber, int pageSize)
        {
            var result = new ServiceResponse<List<AdoptionContractRes>>();
            try
            {
                var contractEntities = await _repo.GetAllAdoptionContracts(pageNumber, pageSize);

                var adoptionContractsList = _mapper.Map<List<AdoptionContractRes>>(contractEntities);

                result.Data = adoptionContractsList;
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.InnerException != null
                    ? e.InnerException.Message + "\n" + e.StackTrace
                    : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ServiceResponse<AdoptionContractRes>> CreateAdoptionContract(AdoptionContractReq adoptionContract)
        {
            var result = new ServiceResponse<AdoptionContractRes>();
            try
            {
                var applicationExist = await _repo.GetAdoptionContractByApplicationId(adoptionContract.ApplicationId);
                if (applicationExist != null)
                {
                    result.Success = false;
                    result.Message = "Application with the same id already exists!";
                    return result;
                }

                // Map DTO to entity
                var newContract = _mapper.Map<AdoptionContract>(adoptionContract);  // If AutoMapper is configured, this should work as expected

                // Insert the new cat into the repository
                await _repo.AddAsync(newContract);

                // After the entity is saved, return the saved entity's details
                var savedContract = _mapper.Map<AdoptionContractRes>(newContract);  // Map to response DTO

                result.Data = savedContract;
                result.Success = true;
                result.Message = "Adoption Contract created successfully!";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.InnerException != null
                    ? $"{e.InnerException.Message}\n{e.StackTrace}"
                    : $"{e.Message}\n{e.StackTrace}";
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteAdoptionContract(int id)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                var contractExist = await _repo.GetAdoptionContractById(id);
                if (contractExist == null)
                {
                    result.Success = false;
                    result.Message = "Adoption Contract not found";
                    result.Data = false;
                }
                else
                {
                    await _repo.Remove(contractExist);
                    result.Success = true;
                    result.Message = "Deleted successfully";
                    result.Data = true;  // Deletion successful
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
                result.Data = false;  // An error occurred, deletion unsuccessful
            }

            return result;
        }

        public async Task<ServiceResponse<AdoptionContractRes>> GetAdoptionContractByIdAsync(int id)
        {
            var result = new ServiceResponse<AdoptionContractRes>();
            try
            {
                var contract = await _repo.GetAdoptionContractById(id);
                if (contract == null)
                {
                    result.Success = false;
                    result.Message = "Adoption Contract not found";
                }
                else
                {
                    var resMaterial = _mapper.Map<AdoptionContract, AdoptionContractRes>(contract);

                    result.Data = resMaterial;
                    result.Success = true;
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.InnerException != null
                    ? e.InnerException.Message + "\n" + e.StackTrace
                    : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }


         public async Task<ServiceResponse<AdoptionContractRes>> UpdateAdoptionContract(AdoptionContractReq adoptionContract, int id)
        {
            var result = new ServiceResponse<AdoptionContractRes>();
            try
            {
                ArgumentNullException.ThrowIfNull(adoptionContract);

                // Fetch the existing cat entity from the repository
                var contractUpdate = await _repo.GetAdoptionContractById(id)
                                 ?? throw new ArgumentException("Adoption Contract doesn't exist!");

                var applicationExist = await _repo.GetAdoptionContractByApplicationId(adoptionContract.ApplicationId);
                if (applicationExist != null)
                {
                    result.Success = false;
                    result.Message = "Application with the same id already exists!";
                    return result;
                }

                // Use AutoMapper to map the DTO to the existing entity
                _mapper.Map(adoptionContract, contractUpdate); // Update properties in catUpdate with values from updateForm

                await _repo.Update(contractUpdate); // Assuming _Repo.Update saves changes to the database

                // Optionally return the updated DTO
                result.Data = _mapper.Map<AdoptionContractRes>(contractUpdate);
                result.Success = true;
                result.Message = "Updated Adoption Contract successfully";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.InnerException != null
                    ? e.InnerException.Message + "\n" + e.StackTrace
                    : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }

    }
}
