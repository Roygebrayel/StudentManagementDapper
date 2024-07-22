using AutoMapper;
using SchoolManagement.Application.DTO;
using StudentManagenent.Domain.Entities;


namespace StudentManagement.Services.Mappings
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
        }
    }
}
