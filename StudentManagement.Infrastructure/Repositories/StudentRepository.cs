
using Dapper;
using Npgsql;
using StudentManagenent.Domain.Entities;
using System.Data;

namespace StudentManagement.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new NpgsqlConnection(_connectionString);

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            var dbconnection = Connection;
            const string query = @"SELECT * FROM public.""Students"" ";

            var students = await dbconnection.QueryAsync<Student>(query);
            return students;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var dbconnection = Connection;
            const string query = @"SELECT * FROM public.""Students"" WHERE ""id""=@Id ";

            var student = await dbconnection.QueryFirstOrDefaultAsync<Student>(query, new
            {
                Id = id
            });

            return student!;
        }

        public async Task AssignCourseToStudentAsync(int studentId, int courseId)
        {
            using var dbConnection = Connection;
            const string query = @"INSERT INTO public.""Students_Courses_Mapper"" (""student_id"", ""course_id"") 
                                   VALUES (@studentId, @courseId)";
            await dbConnection.ExecuteAsync(query, new
            {
                studentId = studentId,
                courseId = courseId
            });
        }

        public async Task InsertStudentAsync(Student student)
        {
            var dbconnection = Connection;
            const string query = @"INSERT INTO public.""Students"" (""name"", ""email"", ""age"") 
                                    VALUES(@Name, @Email, @Age) ";
            await dbconnection.ExecuteAsync(query, new
            {
                student.Name,
                student.Email,
                student.Age
            });
        }

        public async Task UpdateStudentAsync(Student student)
        {
            var dbConnection = Connection;
            const string query = @"UPDATE public.""Students"" 
                                SET ""name"" = @Name, 
                                ""email"" =  @Email, 
                                ""age"" = @Age ";
            await dbConnection.ExecuteAsync(query, student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            var dbconnection = Connection;

            const string deleteStudent = @"DELETE FROM public.""Students"" WHERE ""id"" = @Id ";
        
            await dbconnection.ExecuteAsync(deleteStudent, new
            {
                Id = id
            });

        }
    }
}
