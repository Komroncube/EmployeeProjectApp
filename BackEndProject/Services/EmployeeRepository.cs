using Microsoft.Data.SqlClient;

namespace BackEndProject.Services;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DateTime defaultDatetime;
    private SqlConnection connection;

    public EmployeeRepository(string connectionString)
    {
        this.connection = new SqlConnection(connectionString);
        connection.Open();
        this.defaultDatetime = new DateTime(1753, 1, 1);
    }

    public int Create(Employee employee)
    {
        string query = "INSERT INTO [dbo].[Employees] ([name], [Surname], [Email], [Login], [Password], [Role])" +
                                    "VALUES" +
                                    $"('{employee.Name}', '{employee.Surname}', '{employee.Email}', '{employee.Login}', '{employee.Password}', {(int)employee.Role})";
        SqlCommand command = new SqlCommand(query, connection);
        return command.ExecuteNonQuery();


    }

    public int Delete(int id)
    {
        string query = $"Delete from employees where id = {id}";
        SqlCommand command = new SqlCommand(query, connection);
        return command.ExecuteNonQuery();
    }

    public List<Employee> GetAll()
    {
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
                employee.ModifyDate = nulldate==DateTime.MinValue ? defaultDatetime : nulldate;

                check = DateTime.TryParse(reader[10].ToString(), out nulldate);
                employee.DeletedDate = nulldate == DateTime.MinValue ? defaultDatetime : nulldate;

                employees.Add(employee);

            }
        }
        return employees;

    }

    public Employee GetById(int id)
    {
        string query = $"Select * from Employees where id={id} and status<3";

        SqlCommand command = new SqlCommand(query, connection);
        using (SqlDataReader reader = command.ExecuteReader())
        {
            if (reader.Read())
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
                employee.ModifyDate = nulldate == DateTime.MinValue ? defaultDatetime : nulldate;

                check = DateTime.TryParse(reader[10].ToString(), out nulldate);
                employee.DeletedDate = nulldate == DateTime.MinValue ? defaultDatetime : nulldate;

                return employee;

            }
        }
        return null;
    }

    public int Update(int id, Employee employee)
    {
        string query = "UPDATE Employees SET " +
                                "name = @Name, " +
                                "surname = @Surname, " +
                                "email = @Email, " +
                                "login = @Login, " +
                                "password = @Password, " +
                                "role = @Role, " +
                                "status = @Status, " +
                                "modifydate = @ModifyDate," +
                                "deleteddate = @DeletedDate " +
                                "WHERE id = @Id";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", employee.Name);
        command.Parameters.AddWithValue("@Surname", employee.Surname);
        command.Parameters.AddWithValue("@Email", employee.Email);
        command.Parameters.AddWithValue("@Login", employee.Login);
        command.Parameters.AddWithValue("@Password", employee.Password);
        command.Parameters.AddWithValue("@Role", (int)employee.Role);
        command.Parameters.AddWithValue("@Status", (int)employee.Status);
        command.Parameters.AddWithValue("@ModifyDate", employee.ModifyDate);
        command.Parameters.AddWithValue("@DeletedDate", employee.DeletedDate);
        command.Parameters.AddWithValue("@Id", employee.Id);
        return command.ExecuteNonQuery();

    }
}
