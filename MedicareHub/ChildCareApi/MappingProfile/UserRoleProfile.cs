
using AutoMapper;
using ChildCare.DTOs;

namespace ChildCareApi.MappingProfile
{
    public class UserRoleProfile:Profile
    {
        public UserRoleProfile() {
            CreateMap<ChildCareCore.Entities.UserRole, UserRoleViewModel>();
            CreateMap<RegistrationViewModel, ChildCareCore.Entities.User>();

        }

    }
}
