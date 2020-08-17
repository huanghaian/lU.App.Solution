using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestWebApiSample.Entity;
using TestWebApiSample.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace TestWebApiSample.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly INpoiWordProvider _wordProvider;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, INpoiWordProvider npoiWordProvider)
        {
            _logger = logger;
            _wordProvider = npoiWordProvider;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            var model = new WrodHandler();
            model.ReadWord();
            var rootPath = $"C:\\WordTemp";

            var files = Directory.GetFiles(rootPath, "*.docx", SearchOption.AllDirectories);
            if (files == null)
                throw new ArgumentException("文件夹下的word文件不存在！");
            var dataResult = new List<string>();
            foreach (var path in files)
            {
                var result = _wordProvider.GetTextInTable(path);
                if (result.Count > 0)
                {
                    dataResult.AddRange(result);
                }
            }
            var excelProvider = HttpContext.RequestServices.GetService<INpoiExcelProvider>();
            excelProvider.CreateExcel(dataResult,'\t',null);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [Authorize]
        public IEnumerable<WeatherForecast> GetValues()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
