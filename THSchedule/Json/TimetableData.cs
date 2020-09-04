using System.Collections.Generic;

namespace THSchedule
{
    public class TimetableData
    {
        public List<Timetable> Timetable { get; set; }
        public List<EventDetail> Events { get; set; }
        public List<string> Timezones { get; set; }
    }
}
