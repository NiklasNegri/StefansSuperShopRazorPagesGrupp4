using AutoMapper;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Data.DTOs;

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
