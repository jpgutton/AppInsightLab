using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<WeatherForecast>> Get()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 10);

            if(num > 9)
            {
                return NotFound();
            }
            else if (num > 7 && num < 9)
            {
                return Unauthorized();
            }
            else if (num == 6)
            {
                Task.Delay(2000).Wait();
                string msg = "Exception request";
                return StatusCode((int)HttpStatusCode.InternalServerError, msg);
            }
            else
            {
                var rng = new Random();
                var weatherlist = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToList();

                return weatherlist;
            }
        }
    }
}
