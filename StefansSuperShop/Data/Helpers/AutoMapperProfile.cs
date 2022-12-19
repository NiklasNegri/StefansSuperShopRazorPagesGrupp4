using AutoMapper;
using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Data.Entities;

namespace StefansSuperShop.Data.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //ApplicationUserDTO -> ApplicationUser
            CreateMap<ApplicationUserDTO, ApplicationUser>();
        }
    }
}
