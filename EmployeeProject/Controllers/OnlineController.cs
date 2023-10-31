using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetActionResult()
        {
            using(HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync("https://jsonplaceholder.typicode.com/comments").Result.Content.ReadAsStringAsync();
                return Ok(result);
            }
        }
    }
}
