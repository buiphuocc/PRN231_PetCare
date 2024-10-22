using Application.IService;
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
		private readonly IMapper _mapper;

		public AdoptionApplicationService(IAdoptionApplicationRepo repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
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

		public async Task<ServiceResponse<List<AdoptionApplicationRes>>> GetAllApplications()
		{
			var result = new ServiceResponse<List<AdoptionApplicationRes>>();
			try
			{
				var contractEntities = await _repo.GetAllAsync();

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
					await _repo.DeleteAsync(id);

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

				await _repo.UpdateAsync(applicationUpdate); // Assuming _Repo.Update saves changes to the database

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
	}
}
