using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToddlerCalendar
{
    public class Holidays
    {
        public class Holiday
        {
            public DateTime Date { get; set; }
            public string Name { get; set; }
            public string HtmlColor { get; set; }
            public Holiday(string name, DateTime date, string htmlColor = "#0e7abf")
            {
                Date = date;
                Name = name;
                HtmlColor = htmlColor;
            }
        }
        private static DateTime today = DateTime.Now;
        public static List<Holiday> GetAll()
        {
            var holidaysArray = new[]
            {
                new Holiday("New Year's Day", GetHoliday(1, 1, true)),
                new Holiday("MLK Day", GetNthDayOfMonth(3, DayOfWeek.Monday, 1, today.Year)),
                new Holiday("Groundhog Day", GetHoliday(2, 2)),
                new Holiday("Valentine's Day", GetHoliday(14, 2)),
                new Holiday("Washington's Birthday", GetNthDayOfMonth(3, DayOfWeek.Monday, 2, today.Year)),
                new Holiday("Easter", EasterSunday(today.Year)),
                new Holiday("Memorial Day", GetLastDayOfMonth(DayOfWeek.Monday, 5, today.Year)),
                new Holiday("4th of July", GetHoliday(4, 7)),
                new Holiday("Labor Day", GetNthDayOfMonth(1, DayOfWeek.Monday, 9, today.Year)),
                new Holiday("Columbus Day", GetNthDayOfMonth(2, DayOfWeek.Monday, 10, today.Year)),                
                new Holiday("Veteran's Day", GetHoliday(11, 11)),
                new Holiday("Thanksgiving", GetNthDayOfMonth(4, DayOfWeek.Thursday, 11, today.Year)),
                //new Holiday("Day after Thanksgiving", GetNthDayOfMonth(4, DayOfWeek.Thursday, 11, today.Year).AddDays(1)),
                new Holiday("Christmas Eve", GetHoliday(24, 12)),
                new Holiday("Christmas", GetHoliday(25, 12))                
            };

            return holidaysArray.ToList();
        }

        public static DateTime EasterSunday(int year)
        {
            int day = 0;
            int month = 0;

            int g = year % 19;
            int c = year / 100;
            int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }
            
            return new DateTime(year, month, day);
        }

        private static DateTime GetHoliday(int day, int month, bool nextYear = false)
        {
            var year = today.Year;
            if (nextYear)
                year += 1;
            var holiday = new System.DateTime(year, month, day);
            return GetHolidayDayIfOnSaturdayOrSunday(holiday);
        }

        private static System.DateTime GetNthDayOfMonth(int number, DayOfWeek day, int month, int year)
        {
            var welooking = new DateTime(year, month, 1);
            while (welooking.DayOfWeek != day)
            {
                welooking = welooking.AddDays(1);
            }
            return welooking.AddDays(7 * (number - 1));
        }

        private static System.DateTime GetLastDayOfMonth(DayOfWeek day, int month, int year)
        {
            var welooking = new DateTime(year, month + 1, 1).AddDays(-1);
            while (welooking.DayOfWeek != day)
            {
                welooking = welooking.AddDays(-1);
            }
            return welooking;
        }

        private static DateTime GetHolidayDayIfOnSaturdayOrSunday(DateTime holiday)
        {
            if (holiday.Day == 24 && holiday.Month == 12 && holiday.DayOfWeek == DayOfWeek.Sunday)
            {
                return holiday.AddDays(-2);
            }
            if (holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                return holiday.AddDays(-1);
            }
            else if (holiday.DayOfWeek == DayOfWeek.Sunday)
            {
                return holiday.AddDays(1);
            }
            else
            {
                return holiday;
            }
        }
    }
}