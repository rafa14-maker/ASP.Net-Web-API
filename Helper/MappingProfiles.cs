using Akij_Bashir_It.Dto;
using Akij_Bashir_It.Models;
using AutoMapper;

namespace Akij_Bashir_It.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
