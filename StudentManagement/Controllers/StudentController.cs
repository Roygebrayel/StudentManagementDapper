
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTO;
using StudentManagement.Services.Services;

namespace Courses_StudentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private  readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudentsAsync()
        { 
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("GetStudentById")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            return Ok(student);
        }

        [HttpPost("AssignCourseToStudent")]
        public async Task<IActionResult> AssignCourseToStudentAsync(int studentId, int courseId)
        { 
            await _studentService.AssignCourseToStudentAsync(studentId, courseId);
            return Ok("Course assigned to student");
        }

        [HttpPost("InsertStudent")]
        public async Task<IActionResult> InsertStudentAsync([FromBody] StudentDTO studentDTO)
        {
            await _studentService.InsertStudentAsync(studentDTO);
            return Ok("Student inserted successfully");
        }

        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudentAsync([FromBody] StudentDTO studentDTO)
        {
            await _studentService.UpdateStudentAsync(studentDTO);
            return Ok("Student updated succesfully");
        }

        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(int id)
        { 
            await _studentService.DeleteStudentAsync(id);
            return Ok("Student deleted successfully");
        }
    }
}
