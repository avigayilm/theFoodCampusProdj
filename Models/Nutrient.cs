using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace theFoodCampus.Models
{
    public class Nutrient
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
        public class Root
        {
            public string Name { get; set; }
            public string UnitName { get; set; }
            public double Value { get; set; }
            public int? DailyPercent { get; set; }
        }
    }
}
