using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace SatnogsApi
{
    public interface ISatnogsApi
    {
        [Get("/api/telemetry/?format=json&satellite={id}")]
        Task<List<TelemetryEntry>> GetTelemetry([AliasAs("id")] int satelliteId);
    }
}
