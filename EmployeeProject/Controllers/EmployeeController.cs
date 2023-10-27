using BackEndProject.DTOs;
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
    public string Post([FromForm] EmployeeDto employeeDto)
    {
        return employeeService.CreateEmployee(employeeDto);
    }

    // PUT api/<EmployeeController>/5
    [HttpPut("{id}")]
    public string Put(int id, [FromForm] EmployeeDto employeeDto)
    {
        return employeeService.UpdateEmployee(id, employeeDto);
    }

    // DELETE api/<EmployeeController>/5
    [HttpPatch("{id}")]
    public string Delete(int id)
    {
        return employeeService.DeleteEmployee(id);
    }
    [HttpDelete("{id}")]
    public string RootDelete(int id)
    {
        return employeeService.RootDeleteEmployee(id);
    }
}
