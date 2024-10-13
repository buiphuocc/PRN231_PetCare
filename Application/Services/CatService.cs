using Application.IService;
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.ServiceResponse;
using Infrastructure.Ultilities;
using Infrastructure.ViewModels.CatDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CatService : ICatService
    {
        private readonly ICatRepo _Repo;
        private readonly IMapper _mapper;

        public CatService(IMapper mapper, ICatRepo Repo)
        {
            _mapper = mapper;
            _Repo = Repo;
        }

        public async Task<ServiceResponse<List<CatResDTO>>> GetAll(int pageNumber, int pageSize)
        {
            var result = new ServiceResponse<List<CatResDTO>>();
            try
            {
                // Fetch all Cat entities from the repository with pagination
                var catEntities = await _Repo.GetAllCats(pageNumber, pageSize);

                // Use AutoMapper to map entities to DTOs
                var catList = _mapper.Map<List<CatResDTO>>(catEntities);

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





        public async Task<ServiceResponse<CatResDTO>> GetById(int id)
        {
            var result = new ServiceResponse<CatResDTO>();
            try
            {
                var cat = await _Repo.GetCatById(id);
                if (cat == null)
                {
                    result.Success = false;
                    result.Message = "Cat not found";
                }
                else
                {
                    var resMaterial = _mapper.Map<Cat, CatResDTO>(cat);

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

        public async Task<ServiceResponse<CatResDTO>> Create(CatReqDTO createForm)
        {
            var result = new ServiceResponse<CatResDTO>();
            try
            {
                var catExist = await _Repo.GetCatByName(createForm.Name);
                if (catExist != null)
                {
                    result.Success = false;
                    result.Message = "Cat with the same name already exist!";
                }
                else
                {
                    var newCat = _mapper.Map<CatReqDTO, Cat>(createForm);
                    newCat.CatId = 0;
                    await _Repo.AddAsync(newCat);
                    result.Data = new CatResDTO();
                    result.Success = true;
                    result.Message = "Cat created successfully!";
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

        public async Task<ServiceResponse<CatResDTO>> Update(CatReqDTO updateForm, int catId)
        {
            var result = new ServiceResponse<CatResDTO>();
            try
            {
                ArgumentNullException.ThrowIfNull(updateForm);

                // Fetch the existing cat entity from the repository
                var catUpdate = await _Repo.GetCatById(catId)
                                 ?? throw new ArgumentException("Given cat Id doesn't exist!");

                // Use AutoMapper to map the DTO to the existing entity
                _mapper.Map(updateForm, catUpdate); // Update properties in catUpdate with values from updateForm

                await _Repo.Update(catUpdate); // Assuming _Repo.Update saves changes to the database

                // Optionally return the updated DTO
                result.Data = _mapper.Map<CatResDTO>(catUpdate);
                result.Success = true;
                result.Message = "Updated material successfully";
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
                var catExist = await _Repo.GetCatById(catId);
                if (catExist == null)
                {
                    result.Success = false;
                    result.Message = "Cat not found";
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
