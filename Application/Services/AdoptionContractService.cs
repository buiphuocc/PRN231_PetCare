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
        public async Task<ServiceResponse<List<AdoptionContractRes>>> GetAllAdoptionContractsAsync()
        {
            var result = new ServiceResponse<List<AdoptionContractRes>>();
            try
            {
                // Fetch all Cat entities from the repository with pagination
                var contractEntities = await _repo.GetAllAdoptionContracts();

                // Use AutoMapper to map entities to DTOs
                var adoptionList = _mapper.Map<List<AdoptionContractRes>>(contractEntities);

                result.Data = adoptionList;
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
                // Check if a cat with the same name already exists
                var catExist = await _repo.GetAdoptionContractByApplicationId(adoptionContract.ApplicationId);
                if (catExist != null)
                {
                    result.Success = false;
                    result.Message = "Cat with the same id already exists!";
                    return result;
                }

                // Map DTO to entity
                var newCat = _mapper.Map<AdoptionContract>(adoptionContract);  // If AutoMapper is configured, this should work as expected

                // Insert the new cat into the repository
                await _repo.AddAsync(newCat);

                // After the entity is saved, return the saved entity's details
                var savedCat = _mapper.Map<AdoptionContractRes>(newCat);  // Map to response DTO

                result.Data = savedCat;
                result.Success = true;
                result.Message = "Cat Profile created successfully!";
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

        public async Task<ServiceResponse<bool>> Delete(int catId)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                var catExist = await _repo.GetAdoptionContractById(catId);
                if (catExist == null)
                {
                    result.Success = false;
                    result.Message = "Cat profile not found";
                    result.Data = false;
                }
                else
                {
                    await _repo.Remove(catExist);
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
                var cat = await _repo.GetAdoptionContractById(id);
                if (cat == null)
                {
                    result.Success = false;
                    result.Message = "Adoption Contract not found";
                }
                else
                {
                    var resMaterial = _mapper.Map<AdoptionContract, AdoptionContractRes>(cat);

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
                var catUpdate = await _repo.GetAdoptionContractById(id)
                                 ?? throw new ArgumentException("Adoption Contract doesn't exist!");

                // Use AutoMapper to map the DTO to the existing entity
                _mapper.Map(adoptionContract, catUpdate); // Update properties in catUpdate with values from updateForm

                await _repo.Update(catUpdate); // Assuming _Repo.Update saves changes to the database

                // Optionally return the updated DTO
                result.Data = _mapper.Map<AdoptionContractRes>(catUpdate);
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
