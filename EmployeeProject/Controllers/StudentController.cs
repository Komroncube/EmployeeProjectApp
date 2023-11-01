using BackEndProject.Models;
using BackEndProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetStudentById(long id)
        {
            Student student = _studentService.GetStudentById(id);
            return Ok(student);
        }
        [HttpGet]
        public IActionResult GetStudentByYears(int from, int till)
        {
            IEnumerable<Student> students = _studentService.GetStudentsByYears(from, till);
            return Ok(students);
        }
    }
}
