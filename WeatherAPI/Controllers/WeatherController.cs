using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherSerivce _weatherSerivce;

        public WeatherController(WeatherSerivce weatherService)
        {
            _weatherSerivce = weatherService;
        }

        [HttpGet]
        public ActionResult<List<WeatherModel>> Get() => _weatherSerivce.Get();

        [HttpGet("{id:length(24)}", Name = "GetWeather")]
        public ActionResult<WeatherModel> Get(string id)
        {
            var weather = _weatherSerivce.Get(id);

            if (weather == null)
            {
                return NotFound();
            }

            return weather;
        }

        [HttpPost]
        public ActionResult<WeatherModel> Create(WeatherModel weatherModel)
        {
            _weatherSerivce.Create(weatherModel);

            return CreatedAtRoute("GetWeather", new { id = weatherModel.Id.ToString() }, weatherModel);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, WeatherModel weatherModel)
        {
            var weather = _weatherSerivce.Get(id);

            if (weather == null)
            {
                return NotFound();
            }

            _weatherSerivce.Update(id, weatherModel);

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
