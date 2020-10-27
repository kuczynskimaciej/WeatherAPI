using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.Models;
using MongoDB.Driver;

namespace WeatherAPI.Services
{
    public class WeatherSerivce
    {
        private readonly IMongoCollection<Weather> _weather;

        public WeatherSerivce(IWeatherDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _weather = database.GetCollection<Weather>(settings.WeatherCollectionName);
        }

        public List<Weather> Get() => _weather.Find(weather => true).ToList();

        public Weather Get(string id) => _weather.Find(weather => weather.Id == id).FirstOrDefault();

        public Weather Create(Weather weather)
        {
            _weather.InsertOne(weather);
            return weather;
        }

        public void Update(string id, Weather weatherIn) => _weather.ReplaceOne(weather => weather.Id == id, weatherIn);
        public void Remove(Weather weatherIn) => _weather.DeleteOne(weather => weather.Id == weatherIn.Id);
        public void Remove(string id) => _weather.DeleteOne(weather => weather.Id == id);
    }
}
