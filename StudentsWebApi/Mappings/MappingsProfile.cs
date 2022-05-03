using AutoMapper;
using StudentsWebApi.Data;
using StudentsWebApi.Models;

namespace StudentsWebApi.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<StudentModel, Student>()
                .ForMember(s => s.FirstName, opt => opt.MapFrom(o => o.FName))
                .ForMember(s => s.LastName, opt => opt.MapFrom(o => o.LName))
                .ReverseMap();

        }
    }
}
