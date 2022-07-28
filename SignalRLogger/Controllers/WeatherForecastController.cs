using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace SignalRLoggerDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IServiceProvider _serviceProvider;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IServiceProvider isp)
        {
            _logger = logger;
            _serviceProvider = isp;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            
            var ret = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            _logger.LogInformation("return " + ret.Length + " results.");
                        
            return ret;
        }

        /// <summary>
        /// https://localhost:7244/WeatherForecast/readlogs?id=3974d1f2899e
        /// </summary>
        /// <param name="id">容器id</param>
        /// <returns></returns>
        [HttpGet("readlogs")]
        public async Task<string> ReadDockerLogs(string id)
        {
            using Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine($"docker logs {id} --tail=10");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            
            //string output = reader.ReadToEnd();
            int i = 0;
            
            using StreamReader reader = cmd.StandardOutput;
            
            while (!reader.EndOfStream)
            {
                if (i > 3)
                {
                    _logger.LogInformation(await reader.ReadLineAsync());
                }
                i++;
                await Task.Delay(10);
            }
            await cmd.WaitForExitAsync();
            return "ok";
        }
    }
}