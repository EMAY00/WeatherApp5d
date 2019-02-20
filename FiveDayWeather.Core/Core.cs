using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static Android.Hardware.Camera2.CameraCharacteristics;

namespace FiveDayWeatherApp.Core
{
    public class Core
    {

        public static async Task<Forecast> GetWeather(string location)
        {
            string Key = "3fab359fbda15ad56d25958c8c143b17";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?q=" + location + "&APPID=" + Key + "&units=metric";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            var weather = new Forecast
            {
                //if (results["weather"] == null)
                //     return null;

                //weather.Temperature = (string)results["main"]["temp"] + " C";
                //weather.Pressure = (string)results["main"]["pressure"] + " hPa";
                //weather.WindSpeed = (string)results["wind"]["speed"] + " M/S";
                minTemperature = (string)results["main"]["temp_min"],
                maxTemperature = (string)results["main"]["temp_max"],
                Humidity = (string)results["main"]["humidity"],
                Description = (string)results["weather"][0]["main"],
                Wind = (string)results["wind"]["speed"],
                Icon = (string)results["weather"][0]["icon"]


                //weather.ImageName = "_" + (string)results["weather"][0]["icon"];
            };

            return weather;

        }
        public static async Task<List<Forecast>> GetFiveDaysWeather(string location)
        {
            string Key = "3fab359fbda15ad56d25958c8c143b17";
            string queryString = "http://api.openweathermap.org/data/2.5/forecast?q=" + location + "&APPID=" + Key + "&units=metric";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            if (results["list"] == null)
                return null;

            List<Forecast> forecast = new List<Forecast>();

            int currentIterator = 0;
            for (int i = 0; i < 5; i++)
            {

                Forecast weather = new Forecast
                {
                    Date = UnixTimeToString((long)results["list"][currentIterator]["dt"]),
                    minTemperature = (string)results["list"][currentIterator]["main"]["temp_min"] + "°C",
                    maxTemperature = (string)results["list"][currentIterator]["main"]["temp_max"],
                    Description = "_" + (string)results["list"][currentIterator]["weather"][0]["icon"]

                };
                 weather.Date = UnixTimeToString((long)results["list"][currentIterator]["dt"]);

                 currentIterator += 8;
                 forecast.Add(weather);

            }
            return forecast;

        }

        public static string UnixTimeToString(long dt)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(dt).ToLocalTime();

            return dateTime.ToString("dd. MMMM yyyy");
        }
    }

}