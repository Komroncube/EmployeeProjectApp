namespace BackEndProject.Interfaces;

public interface IEmployeeRepository
{
    public Employee GetById(int id);
    public List<Employee> GetAll();
    public int Update(int id, Employee employee);
    public int Delete(int id);
    public int Create(Employee employee);
}
