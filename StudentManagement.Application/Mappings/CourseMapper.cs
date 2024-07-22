using AutoMapper;
using SchoolManagement.Application.DTO;
using StudentManagenent.Domain.Entities;


namespace StudentManagement.Services.Mappings
{
    public class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<Course, CourseDTO>().ReverseMap();
        }
    }
}
