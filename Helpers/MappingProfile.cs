using AutoMapper;
using LogApi.Domain.Entity;
using LogApi.DTO;

namespace LogApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<DataLog, DataLogDTO>().ReverseMap();

        }
    }
}
