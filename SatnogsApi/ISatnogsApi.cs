using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace SatnogsApi
{
    public interface ISatnogsApi
    {
        [Get("/api/telemetry/?format=json&satellite={satelliteId}&page={page}")]
        Task<List<TelemetryEntry>> GetTelemetry(int satelliteId, int? page = null);
    }
}
