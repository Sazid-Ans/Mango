using AutoMapper;
using Mango.Services.ProductApi.Model;
using Mango.Services.ProductApi.Model.Core;
using Mango.Services.ProductApi.Model.Dto;

namespace Mango.Services.ProductApi.AutoMapper
{
    public class MappingProfile : Profile 
    {
        public static MapperConfiguration Mapper()
        {
            var mappingConfif = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
            });
            return mappingConfif;
        } 
    }
}
