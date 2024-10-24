using Infrastructure.ServiceResponse;
using Infrastructure.ViewModels.CatDTO;
using Infrastructure.ViewModels.ShelterDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IShelterService
    {
        public Task<ServiceResponse<PaginationModel<ShelterResDTO>>> GetAll(int pageNumber, int pageSize);
        public Task<ServiceResponse<ShelterResDTO>> GetById(int materialId);
        public Task<ServiceResponse<ShelterResDTO>> Create(ShelterReqDTO createForm);
        public Task<ServiceResponse<ShelterResDTO>> Update(ShelterReqDTO updateForm, int id);
        public Task<ServiceResponse<bool>> Delete(int id);
    }
}
