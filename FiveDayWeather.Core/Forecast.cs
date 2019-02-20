using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FiveDayWeatherApp.Core
{
    public class Forecast
    {
        public string Temperature { get; set; } = " ";
        public string minTemperature { get; set; } = " ";
        public string maxTemperature { get; set; } = " ";
        public string Description { get; set; } = " ";
        public string Humidity { get; set; } = " ";
        public string Wind { get; set; } = " ";
        public string Icon { get; set; } = " ";
        public string Date { get; set; } = " ";

    }
}