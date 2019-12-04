using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;
using Refit;

namespace SatnogsApi
{
    public class TelemetryEntry
    {
        [AliasAs("norad_cat_id")]
        [JsonProperty("norad_cat_id")]
        [Display(Name = "NORAD Catalog Id")]
        public int NoradCatId { get; set; }

        [AliasAs("transmitter")]
        [JsonProperty("transmitter")]
        [Display(Name = "Transmitter")]
        public string Transmitter { get; set; }

        [AliasAs("app_source")]
        [JsonProperty("app_source")]
        [Display(Name = "App Source")]
        public string AppSource { get; set; }

        [AliasAs("decoded")]
        [JsonProperty("decoded")]
        [Display(Name = "Decoded")]
        public string Decoded { get; set; }

        [AliasAs("frame")]
        [JsonProperty("frame")]
        [Display(Name = "Frame data")]
        public string Frame { get; set; }

        [AliasAs("observer")]
        [JsonProperty("observer")]
        [Display(Name = "Observer")]
        public string Observer { get; set; }

        [AliasAs("timestamp")]
        [JsonProperty("timestamp")]
        [Display(Name = "Timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonIgnore]
        [Display(Name = "UNIX Timestamp")]
        public long UnixTimestamp => Timestamp.ToUnixTimeSeconds();
    }
}
