using Infrastructure.ServiceResponse;
using Infrastructure.ViewModels.CatDTO;
using Infrastructure.ViewModels.CatProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface ICatProfileService
    {

        Task<ServiceResponse<List<CatProfileReSDTO>>> GetAll(int pageNumber, int pageSize);
        public Task<ServiceResponse<CatProfileReSDTO>> GetById(int materialId);
        public Task<ServiceResponse<CatProfileReSDTO>> Create(CatProfileReqDTO createForm);
        public Task<ServiceResponse<CatProfileReSDTO>> Update(CatProfileReqDTO updateForm, int id);
        public Task<ServiceResponse<bool>> Delete(int id);
    }
}
