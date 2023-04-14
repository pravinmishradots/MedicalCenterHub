
using AutoMapper;
using ChildCare.DTOs;
using ChildCareApi.DTOs;
using System.Data;

namespace ChildCareApi.MappingProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {         


            CreateMap<ChildCareCore.Entities.User, RegistrationViewModel>();
            CreateMap<CreateUserDto,ChildCareCore.Entities.User>();
            CreateMap<ChildCareCore.Entities.User, UserDto>();
            CreateMap<UerUpdateDto, ChildCareCore.Entities.User>();


        }

   
    }
}


