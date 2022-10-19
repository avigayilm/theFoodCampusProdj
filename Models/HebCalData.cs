using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
namespace theFoodCampus.Models
{
    public class HebCalData
    {
        public class Root
        {
                public Holiday? Holiday { get; set; }
                public int number { get; set; }

                public string HebrewDate { get; set; }
                public string Month { get; set; }
        }
    }
}
