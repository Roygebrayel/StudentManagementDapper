using AutoMapper;
using SchoolManagement.Application.DTO;
using StudentManagement.Infrastructure.Repositories;
using StudentManagenent.Domain.Entities;

namespace StudentManagement.Services.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _courseMapper;

        public CourseService(ICourseRepository courseRepository, IMapper courseMapper)
        {
            _courseRepository = courseRepository;
            _courseMapper = courseMapper;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return _courseMapper.Map<IEnumerable<CourseDTO>>(courses);
        }

        public async Task<CourseDTO> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            return _courseMapper.Map<CourseDTO>(course);
        }

        public async Task<IEnumerable<CourseDTO>> GetCoursesByStudentAgeAsync()
        {
           var courses = await _courseRepository.GetCoursesByStudentAgeAsync();
           return _courseMapper.Map<IEnumerable<CourseDTO>>(courses);
        }

        public async Task UpdateCourseAsync(CourseDTO courseDTO)
        {
            var course =  _courseMapper.Map<Course>(courseDTO);
            await _courseRepository.UpdateCourseAsync(course);
        }

        public async Task InsertCourseAsync(CourseDTO courseDTO)
        {
            var course =  _courseMapper.Map<Course>(courseDTO);
            await _courseRepository.InsertCourseAsync(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _courseRepository.DeleteCourseAsync(id);
        }
    }
}
