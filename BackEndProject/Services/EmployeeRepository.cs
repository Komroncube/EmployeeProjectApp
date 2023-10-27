using BackEndProject.Interfaces;
using Microsoft.Data.SqlClient;

namespace BackEndProject.Services;

public class EmployeeRepository : IEmployeeRepository
{
    private SqlConnection connection;

    public EmployeeRepository()
    {
        this.connection = new SqlConnection("Server=DOTNET-DEVELOPE;Database=EmployeeProject;Trusted_Connection=True;TrustServerCertificate=true;");
    }

    public int Create(Employee employee)
    {
        connection.Open();
        string query = "INSERT INTO [dbo].[Employees] ([name], [Surname], [Email], [Login], [Password], [Role])" +
                                    "VALUES" +
                                    $"({employee.Name}, {employee.Surname}, {employee.Email}, {employee.Login}, {employee.Password}, {(int)employee.Role})" +
                                    "GO";
        SqlCommand command = new SqlCommand(query, connection);
        return command.ExecuteNonQuery();


    }

    public int Delete(int id)
    {
        connection.Open();
        string query = $"Delete from employees where id = {id}";
        SqlCommand command = new SqlCommand(query, connection);
        return command.ExecuteNonQuery();
    }

    public List<Employee> GetAll()
    {
        connection.Open();
        List<Employee> employees = new List<Employee>();
        string query = "Select * from Employees";

        SqlCommand command = new SqlCommand(query, connection);
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Employee employee = new Employee();
                employee.Id = int.Parse(reader[0].ToString());
                employee.Name = reader[1].ToString();
                employee.Surname = reader[2].ToString();
                employee.Email = reader[3].ToString();
                employee.Login = reader[4].ToString();
                employee.Password = reader[5].ToString();
                employee.Status = (Status)int.Parse(reader[6].ToString());
                employee.Role = (Role)int.Parse(reader[7].ToString());
                employee.CreatedDate = DateTime.Parse(reader[8].ToString());

                bool check = DateTime.TryParse(reader[9].ToString(), out DateTime nulldate);
                employee.ModifyDate = nulldate;

                check = DateTime.TryParse(reader[10].ToString(), out nulldate);
                employee.DeletedDate = nulldate;

                employees.Add(employee);

            }
        }
        return employees;

    }

    public Employee GetById(int id)
    {
        connection.Open();
        string query = $"Select * from Employees where id={id}";

        SqlCommand command = new SqlCommand(query, connection);
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Employee employee = new Employee();
                employee.Id = int.Parse(reader[0].ToString());
                employee.Name = reader[1].ToString();
                employee.Surname = reader[2].ToString();
                employee.Email = reader[3].ToString();
                employee.Login = reader[4].ToString();
                employee.Password = reader[5].ToString();
                employee.Status = (Status)int.Parse(reader[6].ToString());
                employee.Role = (Role)int.Parse(reader[7].ToString());
                employee.CreatedDate = DateTime.Parse(reader[8].ToString());

                bool check = DateTime.TryParse(reader[9].ToString(), out DateTime nulldate);
                employee.ModifyDate = nulldate;

                check = DateTime.TryParse(reader[10].ToString(), out nulldate);
                employee.DeletedDate = nulldate;

                return employee;

            }
        }
        return new Employee();
    }

    public int Update(int id, Employee employee)
    {
        throw new NotImplementedException();
    }
}
