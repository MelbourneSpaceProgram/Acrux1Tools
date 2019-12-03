using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Refit;

namespace SatnogsApi
{
    public class TelemetryEntry
    {
        [AliasAs("norad_cat_id")]
        [JsonProperty("norad_cat_id")]
        public int NoradCatId { get; set; }

        [AliasAs("transmitter")]
        [JsonProperty("transmitter")]
        public string Transmitter { get; set; }

        [AliasAs("app_source")]
        [JsonProperty("app_source")]
        public string AppSource { get; set; }

        [AliasAs("decoded")]
        [JsonProperty("decoded")]
        public string Decoded { get; set; }

        [AliasAs("frame")]
        [JsonProperty("frame")]
        public string Frame { get; set; }

        [AliasAs("observer")]
        [JsonProperty("observer")]
        public string Observer { get; set; }

        [AliasAs("timestamp")]
        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonIgnore]
        public long UnixTimestamp => Timestamp.ToUnixTimeSeconds();
    }
}
