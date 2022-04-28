using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using StudentsWebApi.Data;
using Microsoft.AspNetCore.Routing;

namespace StudentsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {

        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentRepository studentRepository;

        public StudentsController(ILogger<StudentsController> logger,
            IStudentRepository studentRepository)
        {
            _logger = logger;
            this.studentRepository = studentRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            Student student = await studentRepository.GetStudentAsync(id);
            if (student != null)
            {
                return Ok(student);
            }
            return BadRequest("Can´t find student");
        }

        [HttpGet()]
        public async Task<ActionResult<Student[]>> GetAll()
        {
            List<Student> students = await studentRepository.GetAllStudentAsync();
            return Ok(students);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Post(Student student)
        {
            var existing = await studentRepository.GetStudentAsync(student.Id);
            if (existing != null) return BadRequest("Id in use");

            Student newStudent=new Student();   
            newStudent.FirstName = student.FirstName;
            newStudent.LastName = student.LastName;
            newStudent.UserName = student.UserName;
            newStudent.Created = DateTime.Now;
            newStudent.Modified = newStudent.Created;
            studentRepository.Add(newStudent);

            if (await studentRepository.SaveChangesAsync())
            {
                return Ok(newStudent); 
            }
            return BadRequest("Failed to save changes");
        }
    }
}