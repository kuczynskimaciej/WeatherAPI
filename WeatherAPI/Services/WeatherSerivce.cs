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
        private readonly IMongoCollection<WeatherModel> _weather;

        public WeatherSerivce(IWeatherDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _weather = database.GetCollection<WeatherModel>(settings.WeatherCollectionName);
        }

        public List<WeatherModel> Get() => _weather.Find(weather => true).Limit(10).SortByDescending(model => model.Hour).ToList();

        public WeatherModel Get(string id) => _weather.Find(weather => weather.Id == id).FirstOrDefault();

        public WeatherModel Create(WeatherModel weatherModel)
        {
            _weather.InsertOne(weatherModel);
            return weatherModel;
        }

        public void Update(string id, WeatherModel weatherModel) => _weather.ReplaceOne(weather => weather.Id == id, weatherModel);
        public void Remove(WeatherModel weatherModel) => _weather.DeleteOne(weather => weather.Id == weatherModel.Id);
        public void Remove(string id) => _weather.DeleteOne(weather => weather.Id == id);
    }
}
