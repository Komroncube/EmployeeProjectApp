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
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
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
                string query = "insert into fruits values(@name, @count, @price);";
                connection.Execute(query, fruitDto);
                return Ok("created");
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

    }
}
