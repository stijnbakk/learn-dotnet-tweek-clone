using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace tweekClone.Models
{
    public class DayOverview
    {
        public DateOnly Date { get; set; }
        public string WeekdayName { get; set; }
        public List<Item> DayItems { get; set; }
    }
    public class WeekOverview
    {
        public List<DayOverview> WeekDays { get; set; }
        public DateOnly StartDate { get; set; }
        public string MonthName => StartDate.ToString("MMMM");
        public int WeekNumber => GetWeekNumber(StartDate);

        private int GetWeekNumber(DateOnly date)
        {
            var dateTime = date.ToDateTime(TimeOnly.FromDateTime(DateTime.Now));
            Calendar calendar = DateTimeFormatInfo.CurrentInfo.Calendar;

            return calendar.GetWeekOfYear(
                dateTime,
                CalendarWeekRule.FirstDay, // Or another rule depending on your needs
                DayOfWeek.Sunday); // Or another day depending on your needs
        }
    }
}