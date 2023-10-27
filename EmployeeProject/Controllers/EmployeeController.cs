using BackEndProject.Interfaces;
using BackEndProject.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService employeeService;

    public EmployeeController(IEmployeeService employeeService) 
    {
        this.employeeService = employeeService;
    }
    // GET: api/<EmployeeController>
    [HttpGet]
    public IEnumerable<Employee> Get()
    {
        return employeeService.GetAllEmployees();
        
    }

    // GET api/<EmployeeController>/5
    [HttpGet("{id}")]
    public Employee? Get(int id)
    {
        var employee = employeeService.GetEmployeeById(id);
        return employee;
    }

    // POST api/<EmployeeController>
    [HttpPost]
    public void Post([FromForm] EmployeeDto employeeDto)
    {
        employeeService.CreateEmployee(employeeDto);
    }

    // PUT api/<EmployeeController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromForm] EmployeeDto employeeDto)
    {
        employeeService.UpdateEmployee(id, employeeDto);
    }

    // DELETE api/<EmployeeController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        employeeService.DeleteEmployee(id);
    }
}
