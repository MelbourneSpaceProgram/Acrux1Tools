using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acrux1Payloads;
using Acrux1Tools.Web.Helpers;
using SatnogsApi;
using SatnogsApi.Models.SatnogsDb;

namespace Acrux1Tools.Web.Models.Telemetry
{
    public class ListTelemetryViewModel
    {
        public int SatelliteId { get; set; }
        public SatelliteEntry Satellite { get; set; }
        public List<TelemetryRow> Telemetry { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
    }

    public class TelemetryRow
    {
        /// <summary>
        /// The raw telemetry data from satnogs
        /// </summary>
        public TelemetryEntry SatnogsTelemetry { get; set; }
        public FecDecodeResult FecDecodeResult { get; set; }
        public BeaconData Acrux1Beacon { get; set; }
    }
}
