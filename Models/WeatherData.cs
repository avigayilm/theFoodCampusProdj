using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
namespace theFoodCampus.Models
{
    public class WeatherData
    {
        public class Root
        {
            public double Temp { get; set; }
            public Weather WeatherFeel { get; set; }
        }
    }
}
