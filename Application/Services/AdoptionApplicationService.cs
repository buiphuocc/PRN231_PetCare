﻿using Application.IService;
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.ServiceResponse;
using Infrastructure.ViewModels.AdoptionApplicationDTO;

namespace Application.Services
{
	public class AdoptionApplicationService : IAdoptionApplicationService
	{
		private readonly IAdoptionApplicationRepo _repo;
		private readonly ICatRepo _catRepo;
		private readonly IMapper _mapper;

		public AdoptionApplicationService(IAdoptionApplicationRepo repo, ICatRepo catRepo, IMapper mapper)
		{
			_repo = repo;
			_catRepo = catRepo;	
			_mapper = mapper;
		}

        public async Task<ServiceResponse<bool>> ApproveAdoptionApplication(int id)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                if (await _repo.GetByIdAsync(id) == null)
                {
                    result.Success = false;
                    result.Message = "Application not found.";
                    result.Data = false;
                }
                else
                {
                    await _repo.ApproveAdoptionApplication(id);

                    result.Success = true;
                    result.Message = "Approved successfully";
                    result.Data = true;
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
				result.Data = false;
            }

            return result;
        }

        public async Task<ServiceResponse<AdoptionApplicationRes>> CreateApplication(AdoptionApplicationReq req)
		{
			var result = new ServiceResponse<AdoptionApplicationRes>();
			
			try
			{
				var application = await _repo.FindApplicationByAdopterAndCatId(req.AdopterId, req.CatId);
				
				if (application != null)
				{
					result.Success = false;
					result.Message = "Application of this user for this cat was found.";
					return result;
				}

				var newApplication = _mapper.Map<AdoptionApplication>(req);
				newApplication.ApplicationStatus = "Pending";

				await _repo.AddAsync(newApplication);

				var savedApplication = _mapper.Map<AdoptionApplicationRes>(newApplication);

				result.Data = savedApplication;
				result.Success = savedApplication != null;
				result.Message = "Adoption application created successfully.";

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

		public async Task<ServiceResponse<List<AdoptionApplicationRes>>> GetAllApplications(int pageNumber, int pageSize)
		{
			var result = new ServiceResponse<List<AdoptionApplicationRes>>();
			try
			{
				var contractEntities = await _repo.GetAllAsync(pageNumber, pageSize);

				var adoptionList = _mapper.Map<List<AdoptionApplicationRes>>(contractEntities);

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

		public async Task<ServiceResponse<AdoptionApplicationRes>> GetApplicationById(int id)
		{
			var result = new ServiceResponse<AdoptionApplicationRes>();
			try
			{
				var application = await _repo.GetByIdAsync(id);
				if (application == null)
				{
					result.Success = false;
					result.Message = "Adoption application not found";
				}
				else
				{
					var resMaterial = _mapper.Map<AdoptionApplication, AdoptionApplicationRes>(application);

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

		public async Task<ServiceResponse<bool>> RemoveApplication(int id)
		{
			var result = new ServiceResponse<bool>();

			try
			{
				var applicationCheck = await _repo.GetByIdAsync(id);

				if (applicationCheck == null)
				{
					result.Success = false;
					result.Message = "Application not found.";
					result.Data = false;
				}
				else
				{
					await _repo.Remove(applicationCheck);

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

		public async Task<ServiceResponse<AdoptionApplicationRes>> UpdateApplication(AdoptionApplicationReq req, int id)
		{
			var result = new ServiceResponse<AdoptionApplicationRes>();
			try
			{
				ArgumentNullException.ThrowIfNull(req);

				// Fetch the existing cat entity from the repository
				var applicationUpdate = await _repo.GetByIdAsync(id)
								 ?? throw new ArgumentException("Adoption application doesn't exist!");

				// Use AutoMapper to map the DTO to the existing entity
				_mapper.Map(req, applicationUpdate); // Update properties in catUpdate with values from updateForm

				await _repo.Update(applicationUpdate); // Assuming _Repo.Update saves changes to the database

				// Optionally return the updated DTO
				result.Data = _mapper.Map<AdoptionApplicationRes>(applicationUpdate);
				result.Success = true;
				result.Message = "Updated Adoption application successfully";
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
        public async Task<ServiceResponse<List<AdoptionHistoryRes>>> GetAllApplicationsByAdopterId(int adopterId)
        {
            var result = new ServiceResponse<List<AdoptionHistoryRes>>();
            try
            {
                var listAll = await _repo.GetAllAsync();
                var filteredList = listAll.Where(a => a.AdopterId == adopterId).ToList();

                var adoptionList = new List<AdoptionHistoryRes>();

                foreach (var app in filteredList)
                {
                    // Retrieve cat's name from the Cat repository based on CatId
                    var cat = await _catRepo.GetByIdAsync(app.CatId);
                    var catName = cat != null ? cat.Name : "Unknown";

                    // Map to the new DTO
                    adoptionList.Add(new AdoptionHistoryRes
                    {
                        ApplicationId = app.ApplicationId,
                        AdopterId = app.AdopterId,
                        AdoptionFee = app.AdoptionFee,
                        ApplicationDate = app.ApplicationDate,
                        ApplicationStatus = app.ApplicationStatus,
                        AdoptionDate = app.AdoptionDate,
                        CatName = catName
                    });
                }

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

        public async Task<ServiceResponse<AdoptionApplicationRes>> GetApplicationByAdopterAndCatId(int adopterId, int catId)
        {
            var result = new ServiceResponse<AdoptionApplicationRes>();

            try
            {
                var listAll = await _repo.GetAllAsync();
                var filtered = listAll.Where(a => a.AdopterId == adopterId && a.CatId == catId).FirstOrDefault();

                if (filtered == null)
				{
                    result.Success = false;
                    result.Message = "Adoption application of this user for this cat does not exist.";
                }
				else
				{
                    result.Success = true;
                    result.Message = "Adoption application of this user for this cat found.";
					result.Data = _mapper.Map<AdoptionApplicationRes>(filtered);
				}
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
            }

            return result;
        }
    }
}
