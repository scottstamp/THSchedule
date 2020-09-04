using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace THSchedule
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))] public DateTime CreatedAt { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))] public DateTime UpdatedAt { get; set; }
    }
}
