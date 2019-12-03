using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acrux1Tools.Web.Helpers;
using SatnogsApi;

namespace Acrux1Tools.Web.Models.Telemetry
{
    public class ListTelemetryViewModel
    {
        public int SatelliteId { get; set; }
        public List<TelemetryRow> Telemetry { get; set; }
    }

    public class TelemetryRow
    {
        /// <summary>
        /// The raw telemetry data from satnogs
        /// </summary>
        public TelemetryEntry SatnogsTelemetry { get; set; }
        public FecDecodeResult FecDecodeResult { get; set; }

        //TODO: Acrux-1 decoded beacon/packet
    }
}
