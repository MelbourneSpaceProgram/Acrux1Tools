using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acrux1Payloads;
using Acrux1Tools.Web.Helpers;
using SatnogsApi;
using SatnogsApi.Models.SatnogsDb;
using SatnogsApi.Models.SatnogsNetwork;

namespace Acrux1Tools.Web.Models.Telemetry
{
    public class ListTelemetryViewModel
    {
        public int SatelliteId { get; set; }
        public SatelliteEntry Satellite { get; set; }
        public List<TelemetryRow> Telemetries { get; set; }
        public int PageLimit { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
    }

    public class TelemetryRow
    {
        /// <summary>
        /// The observation that this telemetry was decoded from
        /// </summary>
        public ObservationEntry Observation { get; set; }
        /// <summary>
        /// The particular demoded data entry that this telemetry row is for
        /// </summary>
        public ObservationEntry.DemodDataEntry DemodData { get; set; }
        /// <summary>
        /// The forward error correction decode attempt result for this demoded data 
        /// </summary>
        public FecDecodeResult FecDecodeResult { get; set; }
        /// <summary>
        /// The final decoded telemetry beacon from this demoded data
        /// </summary>
        public BeaconData Acrux1Beacon { get; set; }
    }
}
