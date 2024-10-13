

using AutoMapper;
using Domain.Entities;
using Infrastructure.ViewModels.CatDTO;
using Infrastructure.ViewModels.UserDTO;

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







        }
    }
}

