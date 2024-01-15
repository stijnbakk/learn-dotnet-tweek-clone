using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}