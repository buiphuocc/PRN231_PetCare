using Infrastructure.ServiceResponse;

using Infrastructure.ViewModels.DonationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IDonationService
    {

        Task<ServiceResponse<List<DonationResDTO>>> GetAll(int pageNumber, int pageSize);
        public Task<ServiceResponse<DonationResDTO>> GetById(int id);
        public Task<ServiceResponse<DonationResDTO>> Create(DonationReqDTO createForm);
        public Task<ServiceResponse<DonationResDTO>> Update(DonationReqDTO updateForm, int id);
        public Task<ServiceResponse<bool>> Delete(int id);


        Task<ServiceResponse<List<DonationResDTO>>> GetDonorsByDonationIdAsync(int donationId);

        Task<ServiceResponse<List<DonationResDTO>>> GetDonationsByDonorIdAsync(int donorId);
    }
}
