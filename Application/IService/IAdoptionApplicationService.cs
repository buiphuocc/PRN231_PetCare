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
		Task<ServiceResponse<List<AdoptionApplicationRes>>> GetAllApplications();
		Task<ServiceResponse<AdoptionApplicationRes>> GetApplicationById(int id);
		Task<ServiceResponse<AdoptionApplicationRes>> UpdateApplication(AdoptionApplicationReq req, int id);
		Task<ServiceResponse<bool>> RemoveApplication(int id);
	}
}
