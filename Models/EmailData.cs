using Newtonsoft.Json;

namespace WorkScheduleMaker.Models
{
    public class EmailData
    {
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("admin")]
        public string Admin { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
    }
}
