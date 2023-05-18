using AutoMapper;
using Core.Entities;
using Core.Models.Product;

namespace Services.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<ProductDto, ProductEntity>().ReverseMap();
        }
    }
}
