using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly WeatherSerivce _weatherSerivce;

        public BooksController(WeatherSerivce weatherService)
        {
            _weatherSerivce = weatherService;
        }

        [HttpGet]
        public ActionResult<List<Weather>> Get() =>
            _weatherSerivce.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Weather> Get(string id)
        {
            var weather = _weatherSerivce.Get(id);

            if (weather == null)
            {
                return NotFound();
            }

            return weather;
        }

        [HttpPost]
        public ActionResult<Weather> Create(Weather weather)
        {
            _weatherSerivce.Create(weather);

            return CreatedAtRoute("GetWeather", new { id = weather.Id.ToString() }, weather);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Weather weatherIn)
        {
            var weather = _weatherSerivce.Get(id);

            if (weather == null)
            {
                return NotFound();
            }

            _weatherSerivce.Update(id, weatherIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var weather = _weatherSerivce.Get(id);

            if (weather == null)
            {
                return NotFound();
            }

            _weatherSerivce.Remove(weather.Id);

            return NoContent();
        }
    }
}
