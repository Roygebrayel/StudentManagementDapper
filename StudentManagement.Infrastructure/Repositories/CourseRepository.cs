using Dapper;
using Npgsql;
using StudentManagenent.Domain.Entities;
using System.Data;

namespace StudentManagement.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly string _connectionString;

        public CourseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            var sql = @"SELECT * FROM public.""Courses"" ";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Course>(sql);
            }
        }


        public async Task<Course> GetCourseByIdAsync(int id)
        {
            using var dbConnection = Connection;
            const string query = @"SELECT * FROM public.""Courses"" WHERE ""id"" = @Id ";

            var course = await dbConnection.QueryFirstOrDefaultAsync<Course>(query, new 
            { 
                Id = id
            });
            return course!;
        }

        public async Task<IEnumerable<Course>> GetCoursesByStudentAgeAsync()
        {
            using var dbConnection = Connection;
            const string query =
                @"SELECT c.*
                      FROM public.""Students"" s
                      JOIN public.""Students_Courses_Mapper"" sc ON s.""id"" = sc.""student_id""
                      JOIN public.""Courses"" c ON sc.""course_id"" = c.""id""
                      WHERE s.""age"" > 20";

            var courses = await dbConnection.QueryAsync<Course>(query);
            return courses;
        }

        public async Task UpdateCourseAsync(Course course)
        {
            using var dbConnection = Connection;

            const string query = @" UPDATE public.""Courses"" 
                    SET ""name"" = @Name, ""description"" = @Description
                    WHERE ""id"" = @Id";
            await dbConnection.ExecuteAsync(query, course);

        }

        public async Task InsertCourseAsync(Course course)
        {
            using var dbConnection = Connection;
            const string query = @"INSERT INTO public.""Courses"" (""name"",""description"")
                                 VALUES(@Name, @Description)";

            await dbConnection.ExecuteAsync(query, new
            {
                course.Name,
                course.Description
            });
        }

        public async Task DeleteCourseAsync(int id)
        {
            using var dbConnection = Connection;

            const string deleteCourseFromMapper = @"DELETE FROM public.""Students_Courses_Mapper"" WHERE ""course_id"" = @Id";
            const string deleteStudent = @"DELETE FROM public.""Courses"" WHERE @Id = id";

            await dbConnection.ExecuteAsync(deleteCourseFromMapper, new
            {
                Id = id
            });

            await dbConnection.ExecuteAsync(deleteStudent, new
            {
                Id = id
            });
        }
    }
}
