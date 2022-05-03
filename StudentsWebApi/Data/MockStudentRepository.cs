using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsWebApi.Data
{
    public class MockStudentRepository : IStudentRepository
    {
        private List<Student> _students=new();

        public MockStudentRepository()
        {
            _students.Add(new Student { Id = 1,
                FirstName = "Fredrik",
                LastName = "Terent",
                UserName = "FT",
                Created = DateTime.Parse("2020-02-02 02:02:02"),
                Modified = DateTime.Parse("2022-02-02 02:02:02")
            });

            _students.Add(new Student
            {
                Id = 2,
                FirstName = "Test",
                LastName = "Terent",
                UserName = "TT",
                Created = DateTime.Parse("2020-02-02 02:02:02"),
                Modified = DateTime.Parse("2022-02-02 02:02:02")
            });
        }
        public void Add(Student student)
        {
            _students.Add(student);
        }

        public void Delete(Student student)
        {
            _students.Remove(student);
        }

        public async Task<List<Student>> GetAllStudentAsync()
        {
            return await Task.Run(() => _students.ToList<Student>());
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            var student= await Task.Run(() => _students.FirstOrDefault(o => o.Id == id));
            if (student != null)
                return student; 
            else
                return new Student();
        }

        public async Task<Student> GetStudentByUserNameAsync(string userName)
        {
            var student = await Task.Run(() => _students.FirstOrDefault(
                                    o => o.UserName == userName));
            if (student != null)
                return student;
            else
                return new Student();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
