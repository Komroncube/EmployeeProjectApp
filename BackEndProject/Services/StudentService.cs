using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackEndProject.Services;

public class StudentService
{
    private readonly string connectionString;

    public StudentService(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public Student GetStudentById(long id)
    {
        using(var connection = new SqlConnection(connectionString))
        {
            var parametrs = new DynamicParameters();

            parametrs.Add("id", id);

            Student student = connection.QueryFirstOrDefault<Student>("GetStudentById", parametrs, commandType: CommandType.StoredProcedure);
            return student;
        }
    }
    public IEnumerable<Student> GetStudentsByYears(int beginDate, int endDate)
    {
        using(var connection = new SqlConnection(connectionString))
        {
            var parametrs = new DynamicParameters();
            parametrs.Add("from", beginDate);
            parametrs.Add("till", endDate);

            IEnumerable<Student> students = connection.Query<Student>("GetStudentByYear", parametrs, commandType:CommandType.StoredProcedure);
            return students;
        }
    }
}
