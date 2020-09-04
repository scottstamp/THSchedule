using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace THSchedule
{
    public class Timetable
    {
        public int TimetableId { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public bool IsPerm { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Link { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))] public DateTime CreatedAt { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))] public DateTime UpdatedAt { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))] public DateTime Timestamp { get; set; }
    }
}
