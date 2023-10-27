using BackEndProject.DTOs;

namespace BackEndProject.Interfaces;

public interface IEmployeeService
{
    public List<Employee> GetAllEmployees();
    public Employee GetEmployeeById(int id);
    public string CreateEmployee(EmployeeDto employeeDto);
    public string UpdateEmployee(int id, EmployeeDto employeeDto);
    public string DeleteEmployee(int id);
    public string RootDeleteEmployee(int id);
}
