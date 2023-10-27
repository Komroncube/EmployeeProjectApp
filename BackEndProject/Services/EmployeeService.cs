using BackEndProject.Interfaces;

namespace BackEndProject.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository dbContext;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        this.dbContext = employeeRepository;
    }

    public string CreateEmployee(EmployeeDto employeeDto)
    {
        throw new NotImplementedException();
    }

    public string DeleteEmployee(int id)
    {
        throw new NotImplementedException();
    }

    public List<Employee> GetAllEmployees()
    {
        return dbContext.GetAll();
    }

    public Employee GetEmployeeById(int id)
    {
        throw new NotImplementedException();
    }

    public string UpdateEmployee(int id, EmployeeDto employeeDto)
    {
        throw new NotImplementedException();
    }
}
