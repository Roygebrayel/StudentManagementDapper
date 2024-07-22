using AutoMapper;
using SchoolManagement.Application.DTO;
using StudentManagement.Infrastructure.Repositories;
using StudentManagenent.Domain.Entities;


namespace StudentManagement.Services.Services
{
    public class StudentService : IStudentService
    {
        public readonly IStudentRepository _studentRepositry;
        public readonly IMapper _studentMapper;
        public StudentService(IStudentRepository studentRepositry, IMapper studentMapper)
        {
            _studentRepositry = studentRepositry;
            _studentMapper = studentMapper;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            var students = await _studentRepositry.GetAllStudentsAsync();
            return _studentMapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task<StudentDTO> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepositry.GetStudentByIdAsync(id);
            return _studentMapper.Map<StudentDTO>(student);
        }
        public async Task AssignCourseToStudentAsync(int studentId, int courseId)
        {
            await _studentRepositry.AssignCourseToStudentAsync(studentId, courseId);
        }

        public async Task InsertStudentAsync(StudentDTO studentDTO)
        {
            var student = _studentMapper.Map<Student>(studentDTO);
            await _studentRepositry.InsertStudentAsync(student);
        }

        public async Task UpdateStudentAsync(StudentDTO studentDTO)
        {
            var student =  _studentMapper.Map<Student>(studentDTO);
            await _studentRepositry.UpdateStudentAsync(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _studentRepositry.DeleteStudentAsync(id);
        }
    }
}
