using Microsoft.AspNetCore.Mvc;

namespace JWT_Authentication_and_Role_Based_Authorization.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

       

        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok(Summaries);
        }
    }
}
