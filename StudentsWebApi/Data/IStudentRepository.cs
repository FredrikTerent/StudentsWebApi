using System.Collections.Generic;

namespace StudentsWebApi.Data
{
    public interface IStudentRepository
    {
        // General 
        void Add(Student entity);
        void Delete(Student entity);
        Task<bool> SaveChangesAsync();

        // Student
        Task<List<Student>> GetAllStudentAsync();
        Task<Student> GetStudentAsync(int id);
        Task<Student> GetStudentByUserNameAsync(string userName);
    }
}
