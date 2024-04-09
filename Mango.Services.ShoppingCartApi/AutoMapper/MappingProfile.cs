using AutoMapper;
using Mango.Services.ShoppingCartApi.Model;
using Mango.Services.ShoppingCartApi.Model.Dto;

namespace Mango.Services.ShoppingCartApi.AutoMapper
{
    public class MappingProfile : Profile 
    {
        public static MapperConfiguration Mapper()
        {
            var mappingConfif = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader,CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, cartDetailsDto>().ReverseMap();
            });
            return mappingConfif;
        } 
    }
}
