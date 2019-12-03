using System;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace SatnogsApi
{
    public class TelemetryEntry
    {
        [AliasAs("norad_cat_id")]
        public int NoradCatId { get; set; }

        [AliasAs("transmitter")]
        public string Transmitter { get; set; }

        [AliasAs("app_source")]
        public string AppSource { get; set; }

        [AliasAs("decoded")]
        public string Decoded { get; set; }

        [AliasAs("frame")]
        public string Frame { get; set; }

        [AliasAs("observer")]
        public string Observer { get; set; }

        [AliasAs("timestamp")]
        public DateTimeOffset Timestamp { get; set; }
    }
}
