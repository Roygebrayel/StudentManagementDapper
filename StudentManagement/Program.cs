
using StudentManagement.Infrastructure.Repositories;
using StudentManagement.Services.Mappings;
using StudentManagement.Services.Services;

namespace Courses_StudentsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(StudentMapper));
            builder.Services.AddAutoMapper(typeof(CourseMapper));

            //repos injection
            builder.Services.AddScoped<ICourseRepository, CourseRepository>(provider => new CourseRepository(connectionString!));
            builder.Services.AddScoped<IStudentRepository, StudentRepository>(provider => new StudentRepository(connectionString!));

            //services injection
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IStudentService, StudentService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
