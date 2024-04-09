using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI.AutoMapper
{
    public class MappingProfile : Profile 
    {
        public static MapperConfiguration Mapper()
        {
            var mappingConfif = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>().ReverseMap();
            });
            return mappingConfif;
        } 
    }
}
