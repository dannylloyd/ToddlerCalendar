using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToddlerCalendar.Models
{
    public class Activity
    {
        public string Name { get; set; }
        public string IconFile { get; set; }
        public int IconWidth { get; set; }
        public List<DayOfWeek> DaysToDisplay { get; set; }
    }
}