using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace THSchedule
{
    public class User
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string Habbo { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))] public DateTime CreatedAt { get; set; }
        public string CustomColor { get; set; }
        public int? IconId { get; set; }
        public string IconPosition { get; set; }
        public int? EffectId { get; set; }
        public bool IsAvatarHidden { get; set; }
    }
}
