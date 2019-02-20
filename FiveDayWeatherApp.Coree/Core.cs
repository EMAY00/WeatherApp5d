using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FiveDayWeatherApp.Coree
{
    public class Coree
    {
        const string Key = "e75b73caed0d728414c37cd7037d4291";

        public static async Task<Program> GetWeather(string location)
        {
            string queryString = "http://api.openweathermap.org/data/2.5/weather?q=" + location + "&APPID=" + Key + "&units=metric";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            if (results["weather"] == null)
                return null;

            var weather = new Program();
            weather.Temperature = (string)results["main"]["temp"] + " C";
            weather.Pressure = (string)results["main"]["pressure"] + " hPa";
            weather.WindSpeed = (string)results["wind"]["speed"] + " M/S";

            weather.ImageName = "_" + (string)results["weather"][0]["icon"];
            return weather;
        }

        public static async Task<List<Program>> Get5DaysWeather(string location)
        {
            string queryString = "http://api.openweathermap.org/data/2.5/forecast?q=" + location + "&APPID=" + Key + "&units=metric";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            if (results["list"] == null)
                return null;

            List<Program> weathers = new List<Program>();

            int currentIterator = 0;
            for (int i = 0; i < 5; i++)
            {
                Program weather = new Program();
                weather.Temperature = (string)results["list"][currentIterator]["main"]["temp_max"] + " C";
                weather.TemperatureLow = (string)results["list"][currentIterator]["main"]["temp_min"] + " C";
                weather.ImageName = "_" + (string)results["list"][currentIterator]["weather"][0]["icon"];
                weather.Date = UnixTimeToString((long)results["list"][currentIterator]["dt"]);

                currentIterator += 8;

                weathers.Add(weather);
            }

            return weathers;
        }

        public static string UnixTimeToString(long dt)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(dt).ToLocalTime();

            return dateTime.ToString("dd. MMMM yyyy");
        }
    }
}
