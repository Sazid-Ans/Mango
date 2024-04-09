using AutoMapper;

namespace Mango.Services.EmailApi.AutoMapper
{
    public class MappingProfile : Profile 
    {
        public static MapperConfiguration Mapper()
        {
            var mappingConfif = new MapperConfiguration(config =>
            {
               
            });
            return mappingConfif;
        } 
    }
}
