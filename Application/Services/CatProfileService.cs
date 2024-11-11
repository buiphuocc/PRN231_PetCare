using Application.IService;
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.ServiceResponse;
using Infrastructure.ViewModels.CatDTO;
using Infrastructure.ViewModels.CatProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CatProfileService : ICatProfileService
    {

        private readonly ICatProfileRepo _Repo;
        private readonly IMapper _mapper;

        public CatProfileService(IMapper mapper, ICatProfileRepo Repo)
        {
            _mapper = mapper;
            _Repo = Repo;
        }

        public async Task<ServiceResponse<List<CatProfileReSDTO>>> GetAll(int pageNumber, int pageSize)
        {
            var result = new ServiceResponse<List<CatProfileReSDTO>>();
            try
            {
                // Fetch all Cat entities from the repository with pagination
                var catEntities = await _Repo.GetAllCatPs(pageNumber, pageSize);

                // Use AutoMapper to map entities to DTOs
                var catList = _mapper.Map<List<CatProfileReSDTO>>(catEntities);

                result.Data = catList;
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





        public async Task<ServiceResponse<CatProfileReSDTO>> GetById(int id)
        {
            var result = new ServiceResponse<CatProfileReSDTO>();
            try
            {
                var cat = await _Repo.GetCatPById(id);
                if (cat == null)
                {
                    result.Success = false;
                    result.Message = "Cat Profile not found";
                }
                else
                {
                    var resMaterial = _mapper.Map<CatProfile, CatProfileReSDTO>(cat);

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
        public async Task<ServiceResponse<CatProfileReSDTO>> GetByCatId(int id)
        {
            var result = new ServiceResponse<CatProfileReSDTO>();
            try
            {
                var cat = await _Repo.GetCatPByCatId(id);
                if (cat == null)
                {
                    result.Success = false;
                    result.Message = "Cat Profile not found";
                }
                else
                {
                    var resMaterial = _mapper.Map<CatProfile, CatProfileReSDTO>(cat);

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

        public async Task<ServiceResponse<CatProfileReSDTO>> Create(CatProfileReqDTO createForm)
        {
            var result = new ServiceResponse<CatProfileReSDTO>();
            try
            {
                // Check if a cat with the same name already exists
                var catExist = await _Repo.GetCatPByCatId(createForm.CatId);
                if (catExist != null)
                {
                    result.Success = false;
                    result.Message = "Cat with the same id already exists!";
                    return result;
                }

                // Map DTO to entity
                var newCat = _mapper.Map<CatProfile>(createForm);  // If AutoMapper is configured, this should work as expected

                // Insert the new cat into the repository
                await _Repo.AddAsync(newCat);

                // After the entity is saved, return the saved entity's details
                var savedCat = _mapper.Map<CatProfileReSDTO>(newCat);  // Map to response DTO

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


        public async Task<ServiceResponse<CatProfileReSDTO>> Update(CatProfileReqDTO updateForm, int catId)
        {
            var result = new ServiceResponse<CatProfileReSDTO>();
            try
            {
                ArgumentNullException.ThrowIfNull(updateForm);

                // Fetch the existing cat entity from the repository
                var catUpdate = await _Repo.GetCatPById(catId)
                                 ?? throw new ArgumentException("Given cat profile Id doesn't exist!");

                // Use AutoMapper to map the DTO to the existing entity
                _mapper.Map(updateForm, catUpdate); // Update properties in catUpdate with values from updateForm

                await _Repo.Update(catUpdate); // Assuming _Repo.Update saves changes to the database

                // Optionally return the updated DTO
                result.Data = _mapper.Map<CatProfileReSDTO>(catUpdate);
                result.Success = true;
                result.Message = "Updated cat profile successfully";
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

        public async Task<ServiceResponse<bool>> Delete(int catId)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                var catExist = await _Repo.GetCatPById(catId);
                if (catExist == null)
                {
                    result.Success = false;
                    result.Message = "Cat profile not found";
                    result.Data = false;
                }
                else
                {
                    await _Repo.Remove(catExist);
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


    }
}

