using AutoMapper;
using BackEndProject.DTOs;
using BackEndProject.Interfaces;

namespace BackEndProject.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository dbContext;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this.dbContext = employeeRepository;
        this._mapper = mapper;
    }

    public string CreateEmployee(EmployeeDto employeeDto)
    {
        var employee = _mapper.Map<Employee>(employeeDto);
        var res = dbContext.Create(employee);
        if(res>0)
        {
            return "Employee created";
        }
        return "employee not created";
    }

    public string DeleteEmployee(int id)
    {
        var employee = dbContext.GetById(id);
        if(employee==null)
        {
            return "Employee not found";
        }
        employee.Status=Status.Deleted;
        employee.DeletedDate=DateTime.Now;
        var res = dbContext.Update(id, employee);
        if(res>0) 
        {
            return "Employee deleted";
        }
        return "Employee not returned";
    }

    public List<Employee> GetAllEmployees()
    {
        var employees = dbContext.GetAll();
        employees = employees.Where(x => x.Status != Status.Deleted).ToList();
        return employees;
    }

    public Employee GetEmployeeById(int id)
    {
        var employee = dbContext.GetById(id);
        return employee.Status != Status.Deleted ? employee : null;
    }

    public string RootDeleteEmployee(int id)
    {
        var res = dbContext.Delete(id);
        if(res>0)
        {
            return "Employee deleted";
        }
        return "Error";
    }

    public string UpdateEmployee(int id, EmployeeDto employeeDto)
    {
        var employee = GetEmployeeById(id);
        if (employee == null)
            return "employee updated";
        var updateEmployee = _mapper.Map<Employee>(employee);
        updateEmployee.Id = employee.Id;
        updateEmployee.CreatedDate = employee.CreatedDate;
        updateEmployee.ModifyDate = DateTime.Now;
        updateEmployee.DeletedDate = employee.DeletedDate;
        var res = dbContext.Update(id, updateEmployee);
        if(res>0)
        {
            return "Employee updated";
        }
        return "error";
    }
}
