using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StudentsWebApi.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentContext context;
        private readonly ILogger<StudentRepository> logger;

        public StudentRepository(StudentContext context, ILogger<StudentRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public void Add(Student entity)
        {
            logger.LogInformation($"Adding an object of type student to the context.");
            context.Add(entity);
        }

        public void Delete(Student entity)
        {
            logger.LogInformation($"Removing an object of type student to the context.");
            context.Remove(entity);
        }

        public async Task<List<Student>> GetAllStudentAsync()
        {
            logger.LogInformation($"Getting all Students");

            IQueryable<Student> query = context.Students;

            // Order It
            query = query.OrderByDescending(c => c.Id);

            return await query.ToListAsync<Student>();
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            logger.LogInformation($"Getting a Student for {id}");

            IQueryable<Student> query = context.Students;

            // Query It
            query = query.Where(c => c.Id == id);
            var student = await query.FirstOrDefaultAsync();
            if (student != null)
            {
                return student;
            }
            throw new Exception("Not found");
        }

        public async Task<Student> GetStudentByUserNameAsync(string userName)
        {
            logger.LogInformation($"Getting a Student for {userName}");

            IQueryable<Student> query = context.Students;

            // Query It
            query = query.Where(c => c.UserName == userName);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
