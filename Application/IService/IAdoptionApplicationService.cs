using Infrastructure.ServiceResponse;
using Infrastructure.ViewModels.AdoptionApplicationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
	public interface IAdoptionApplicationService
	{
		Task<ServiceResponse<AdoptionApplicationRes>> CreateApplication(AdoptionApplicationReq req);
		Task<ServiceResponse<List<AdoptionApplicationRes>>> GetAllApplications(int pageNumber, int pageSize);
		Task<ServiceResponse<AdoptionApplicationRes>> GetApplicationById(int id);
		Task<ServiceResponse<AdoptionApplicationRes>> UpdateApplication(AdoptionApplicationReq req, int id);
		Task<ServiceResponse<bool>> RemoveApplication(int id);
		Task<ServiceResponse<List<AdoptionHistoryRes>>> GetAllApplicationsByAdopterId(int adopterId);
		Task<ServiceResponse<AdoptionApplicationRes>> GetApplicationByAdopterAndCatId(int adopterId, int catId);
		Task<ServiceResponse<bool>> ApproveAdoptionApplication(int id);
	}
}
