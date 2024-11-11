

using AutoMapper;
using Domain.Entities;
using Infrastructure.ViewModels.AppointmentDTO;
using Infrastructure.ViewModels.AdoptionContractDTO;
using Infrastructure.ViewModels.CatDTO;
using Infrastructure.ViewModels.CatProfileDTO;
using Infrastructure.ViewModels.UserDTO;
using Infrastructure.ViewModels.AdoptionApplicationDTO;
using Infrastructure.ViewModels.ShelterDTO;
using Infrastructure.ViewModels.DonationDTO;
using Infrastructure.ViewModels.ImageDTO;

namespace Infrastructure.Mappers
{
    
    public class MapperConfigurationsProfile : Profile
    {
      
        public MapperConfigurationsProfile()
        {
            
            CreateMap<Account, RegisterDTO>().ReverseMap();
            CreateMap<Account, LoginUserDTO>().ReverseMap();
            CreateMap<ResetPassDTO, Account>()
                 .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<Account, ResetPassDTO>();
            CreateMap<Account, UserDTO>().ReverseMap();
            CreateMap<Account, UserUpdateDTO>().ReverseMap();
            CreateMap<Account, UserCountDTO>().ReverseMap();

            CreateMap<Cat, CatResDTO>().ReverseMap();
            CreateMap<Cat, CatReqDTO>().ReverseMap();


            CreateMap<CatProfile, CatProfileReqDTO>().ReverseMap();
            CreateMap<CatProfile, CatProfileReSDTO>().ReverseMap();

            CreateMap<Appointment, AppointmentResponse>().ReverseMap();

            CreateMap<AdoptionContract, AdoptionContractReq>().ReverseMap();
            CreateMap<AdoptionContract, AdoptionContractRes>().ReverseMap();

            CreateMap<AdoptionApplication, AdoptionApplicationReq>().ReverseMap();
			CreateMap<AdoptionApplication, AdoptionApplicationRes>().ReverseMap();

            CreateMap<Shelter, ShelterReqDTO>().ReverseMap();
            CreateMap<Shelter, ShelterResDTO>().ReverseMap();

            CreateMap<Donation, DonationReqDTO>().ReverseMap();
            CreateMap<Donation, DonationResDTO>()
            .ForMember(dest => dest.DonorName, opt => opt.MapFrom(src => src.Donor.UserProfile.Name))
            .ReverseMap()
            .ForPath(src => src.Donor.UserProfile.Name, opt => opt.MapFrom(dest => dest.DonorName));





            CreateMap<Account, DonationReqDTO>().ReverseMap();
            CreateMap<Account, DonationResDTO>().ReverseMap();

            CreateMap<EntityImage, ImageDTO>().ReverseMap();



        }
    }
}

