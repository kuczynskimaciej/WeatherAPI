using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherAPI.Models
{
    public class Weather
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }

        [BsonElement("Hour")]
        public string Hour { get; set; }
        public float Temp{ get; set; }
        public float Humidity { get; set; }
    }
}
