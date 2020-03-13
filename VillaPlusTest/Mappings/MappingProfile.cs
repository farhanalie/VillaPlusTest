using AutoMapper;
using VillaPlus.API.Infrastructure.Entities;
using VillaPlus.API.Resources;

namespace VillaPlus.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Cart, CartResource>();
            CreateMap<Item, ItemResource>();
            CreateMap<Product, ProductResource>();
        }
    }
}
