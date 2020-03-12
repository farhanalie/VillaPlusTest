using AutoMapper;
using VillaPlusTest.Infrastructure.Entities;
using VillaPlusTest.Resources;

namespace VillaPlusTest.Mappings
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
