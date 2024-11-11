using Application.IService;
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.ServiceResponse;
using Infrastructure.Ultilities;
using Infrastructure.ViewModels.CatDTO;
using Infrastructure.ViewModels.DonationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepo _Repo;
        private readonly IMapper _mapper;

        public DonationService(IMapper mapper, IDonationRepo Repo)
        {
            _mapper = mapper;
            _Repo = Repo;
        }

        public async Task<ServiceResponse<List<DonationResDTO>>> GetAll(int pageNumber, int pageSize)
        {
            var result = new ServiceResponse<List<DonationResDTO>>();
            try
            {
                // Fetch all Cat entities from the repository with pagination
                var donationEntities = await _Repo.GetAllDonation(pageNumber, pageSize);

                // Use AutoMapper to map entities to DTOs
                var donationList = _mapper.Map<List<DonationResDTO>>(donationEntities);

                result.Data = donationList;
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





        public async Task<ServiceResponse<DonationResDTO>> GetById(int id)
        {
            var result = new ServiceResponse<DonationResDTO>();
            try
            {
                var donation = await _Repo.GetDonationByDonationId(id);
                if (donation == null)
                {
                    result.Success = false;
                    result.Message = "Donation not found";
                }
                else
                {
                    var resDoantion = _mapper.Map<Donation, DonationResDTO>(donation);

                    result.Data = resDoantion;
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

        public async Task<ServiceResponse<DonationResDTO>> Create(DonationReqDTO createForm)
        {
            var result = new ServiceResponse<DonationResDTO>();
            try
            {
                
                var donationExist = await _Repo.GetDonationByDonationId(createForm.DonationId);
                if (donationExist != null)
                {
                    result.Success = false;
                    result.Message = "Donation with the same id already exists!";
                    return result;
                }

                // Map DTO to entity
                var newDonation = _mapper.Map<Donation>(createForm);  // If AutoMapper is configured, this should work as expected

                
                await _Repo.AddAsync(newDonation);

                // After the entity is saved, return the saved entity's details
                var savedDonation = _mapper.Map<DonationResDTO>(newDonation);  // Map to response DTO

                result.Data = savedDonation;
                result.Success = true;
                result.Message = "Donation created successfully!";
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


        public async Task<ServiceResponse<DonationResDTO>> Update(DonationReqDTO updateForm, int catId)
        {
            var result = new ServiceResponse<DonationResDTO>();
            try
            {
                ArgumentNullException.ThrowIfNull(updateForm);

                // Fetch the existing cat entity from the repository
                var donationUpdate = await _Repo.GetDonationByDonationId(catId)
                                 ?? throw new ArgumentException("Given donation Id doesn't exist!");

                // Use AutoMapper to map the DTO to the existing entity
                _mapper.Map(updateForm, donationUpdate); // Update properties in catUpdate with values from updateForm

                await _Repo.Update(donationUpdate); // Assuming _Repo.Update saves changes to the database

                // Optionally return the updated DTO
                result.Data = _mapper.Map<DonationResDTO>(donationUpdate);
                result.Success = true;
                result.Message = "Updated dontion successfully";
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

        public async Task<ServiceResponse<bool>> Delete(int id)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                var donationExist = await _Repo.GetDonationByDonationId(id);
                if (donationExist == null)
                {
                    result.Success = false;
                    result.Message = "Donation not found";
                    result.Data = false;  
                }
                else
                {
                    await _Repo.Remove(donationExist);
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

        public async Task<ServiceResponse<List<DonationResDTO>>> GetDonorsByDonationIdAsync(int donationId)
        {
            var result = new ServiceResponse<List<DonationResDTO>>();
            try
            {
                var donors = await _Repo.GetDonorsByDonationIdAsync(donationId);
                if (donors == null || !donors.Any())
                {
                    result.Success = false;
                    result.Message = "No donors found for this donation.";
                }
                else
                {
                    result.Data = _mapper.Map<List<DonationResDTO>>(donors);
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

        public async Task<ServiceResponse<List<DonationResDTO>>> GetDonationsByDonorIdAsync(int donorId)
        {
            var result = new ServiceResponse<List<DonationResDTO>>();
            try
            {
                var donations = await _Repo.GetDonationsByDonorIdAsync(donorId);
                if (donations == null || !donations.Any())
                {
                    result.Success = false;
                    result.Message = "No donations found for this donor.";
                }
                else
                {
                    result.Data = _mapper.Map<List<DonationResDTO>>(donations);
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
    }
}
