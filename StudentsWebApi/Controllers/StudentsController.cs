using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using StudentsWebApi.Data;
using Microsoft.AspNetCore.Routing;
using StudentsWebApi.Models;
using AutoMapper;

namespace StudentsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {

        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentRepository studentRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public StudentsController(ILogger<StudentsController> logger,
            IStudentRepository studentRepository,
            LinkGenerator linkGenerator,
            IMapper mapper)
        {
            _logger = logger;
            this.studentRepository = studentRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            Student student = await studentRepository.GetStudentAsync(id);
            if (student != null)
            {
                var sm = mapper.Map<StudentModel>(student);
                return Ok(sm);
                //return Ok(new StudentModel
                //{
                //    FName = student.FirstName,
                //    LName = student.LastName,
                //    UserName = student.UserName
                //});

            }
            return BadRequest("Can´t find student");
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<StudentModel>> Get(string userName)
        {
            Student student = await studentRepository.GetStudentByUserNameAsync(userName);
            if (student != null)
            {
                return Ok(new StudentModel { FName=student.FirstName,
                                                    LName=student.LastName,
                                                    UserName=student.UserName});
            }
            return BadRequest("Can´t find student");
        }

        [HttpGet()]
        public async Task<ActionResult<StudentModel[]>> GetAll()
        {
            List<Student> students = await studentRepository.GetAllStudentAsync();
            List<StudentModel> newSM=new List<StudentModel>();
            foreach(Student student in students)
            {
                newSM.Add(new StudentModel
                {
                    UserName = student.UserName,
                    FName = student.FirstName,
                    LName = student.LastName
                });
            }
            return Ok(newSM);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Post(StudentModel studentModel)
        {
            var existing = await studentRepository.GetStudentByUserNameAsync(studentModel.UserName);
            if (existing != null) return BadRequest("UserName in use");

            Student newStudent=new Student();   
            newStudent.FirstName = studentModel.FName;
            newStudent.LastName = studentModel.LName;
            newStudent.UserName = studentModel.UserName;
            newStudent.Created = DateTime.Now;
            newStudent.Modified = newStudent.Created;
            studentRepository.Add(newStudent);

            if (await studentRepository.SaveChangesAsync())
            {
                var location= linkGenerator.GetPathByAction("Post"
                    ,"Students", new { UserName = newStudent.UserName }); 



                return Created(location, newStudent);
            }
            return BadRequest("Failed to save changes");
        }
    }
}