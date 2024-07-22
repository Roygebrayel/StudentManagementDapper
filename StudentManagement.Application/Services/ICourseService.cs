using SchoolManagement.Application.DTO;


namespace StudentManagement.Services.Services
{
    public interface ICourseService
    {
        public Task<IEnumerable<CourseDTO>> GetCoursesByStudentAgeAsync();
        public Task<IEnumerable<CourseDTO>> GetAllCoursesAsync();
        public Task<CourseDTO> GetCourseByIdAsync(int id);
        public Task UpdateCourseAsync(CourseDTO course);
        public Task InsertCourseAsync(CourseDTO course);
        public Task DeleteCourseAsync(int id);
    }
}
