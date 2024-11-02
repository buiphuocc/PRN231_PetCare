
using Infrastructure.ServiceResponse;
using Infrastructure.ViewModels.CatDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
     public interface ICatService
    {
        Task<ServiceResponse<List<CatResDTO>>> GetAll(int pageNumber, int pageSize);
        public Task<ServiceResponse<CatResDTO>> GetById(int id);
        public Task<ServiceResponse<CatResDTO>> Create(CatReqDTO createForm);
        public Task<ServiceResponse<CatResDTO>> Update(CatReqDTO updateForm, int id);
        public Task<ServiceResponse<bool>> Delete(int id);

    }
}
