using BackEndProject.DTOs;
using BackEndProject.Models;
using Dapper;
using EmployeeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FruitController : ControllerBase
    {
        private readonly string? connectionString;

        public FruitController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DapperConnection");
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "select * from fruits";
                var fruits = connection.Query<Fruit>(query).ToList();
                return Ok(fruits);
            }
        }
        [HttpPost]
        public IActionResult CreateFruit([FromForm] FruitDto fruitDto)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "insert into fruits values(@name, @count, @price); SELECT CAST(SCOPE_IDENTITY() as int)";
                var res = connection.Query<int>(query, fruitDto);
                return Ok(res);
            }
        }
        [HttpGet]
        public IActionResult GetMultiple()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"select * from fruits;
                                 select * from vegetables;";
                var results = connection.QueryMultiple(query);

                var fruits = results.Read<Fruit>();
                var vegetables = results.Read<Vegetable>();
                var multiple = new 
                { 
                    fruits = fruits,
                    vegetables = vegetables
                };
                return Ok(multiple);
            }
        }
        [HttpGet]
        public async ValueTask TestTaskAsync()
        {
            using(var connection = new SqlConnection(connectionString))
            {
                //executescalar query bajarib natijani 1-rowini qaytaradi, funksiyalarni chiqarish kerak
                string query = "select avg(count) from fruits;";
                //var avg = await connection.ExecuteScalarAsync<double>(query);
                //avg = connection.ExecuteScalar<double>(query);


                //execute update va delete funksiya uchun ishlatgan yaxshi
                //query = "Update fruits set count = 9999 where id = @Id";
                //var res = connection.Execute(query, new { Id = 3});
                var res = await connection.ExecuteAsync(query, new { Id = 4 });

                // QueryFirst = LINQ.first
                var sql = "SELECT * FROM fruits WHERE Id = @Id";
                var fruit = connection.QueryFirst<Fruit>(sql, new { Id = 1 });

                // QuerySingle = LINQ.single
                sql = "SELECT * FROM fruits WHERE Name = @Name";
                fruit = connection.QuerySingle<Fruit>(sql, new { Name = "Anor" });

                // Query
                sql = "SELECT * FROM fruits WHERE name like '%n%'";
                var fruits = connection.Query<Fruit>(sql);

                sql = "Select * from fruits; Select * from vegetables;";
                using(var multi = connection.QueryMultiple(sql))
                {
                    fruits = multi.Read<Fruit>();
                    var vegetables = multi.ReadFirst<Vegetable>();
                }

                sql = "Select * from fruits where id = 4; Select * from vegetables where id>100;";
                using (var multi = connection.QueryMultiple(sql))
                {
                    var singlefruit = await multi.ReadSingleAsync<Fruit>();
                    var vegetables = multi.ReadSingleOrDefault<Vegetable>();
                }

            }
        }
    }
}
