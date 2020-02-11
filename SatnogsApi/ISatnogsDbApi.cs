using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using SatnogsApi.Models.SatnogsDb;

namespace SatnogsApi
{
    public interface ISatnogsDbApi
    {
        [Get("/api/telemetry/?format=json&satellite={satelliteId}&page={page}")]
        Task<List<TelemetryEntry>> GetTelemetry(int satelliteId, int? page = null);

        [Get("/api/satellites/?norad_cat_id={noradCatId}")]
        Task<List<SatelliteEntry>> GetSatellites(int? noradCatId);
    }
}
