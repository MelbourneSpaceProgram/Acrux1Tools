using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Refit;

namespace SatnogsApi.Models.SatnogsDb
{
    public class SatelliteEntry
    {
        [AliasAs("norad_cat_id")]
        [JsonProperty("norad_cat_id")]
        public int NoradCatId { get; set; }

        [AliasAs("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [AliasAs("names")]
        [JsonProperty("names")]
        public string Names { get; set; }

        [AliasAs("image")]
        [JsonProperty("image")]
        public string Image { get; set; }

        [AliasAs("status")]
        [JsonProperty("status")]
        public string Status { get; set; }

        [AliasAs("decayed")]
        [JsonProperty("decayed")]
        public string Decayed { get; set; }
    }
}
