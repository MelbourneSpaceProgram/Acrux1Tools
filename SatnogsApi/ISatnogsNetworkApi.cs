using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using SatnogsApi.Models.SatnogsNetwork;

namespace SatnogsApi
{
    public interface ISatnogsNetworkApi
    {
        /// <summary>
        /// Get observation data from the SatNOGS network.
        /// </summary>
        /// <param name="observationId">The Id of a specific observation. If null, returns all matching observations.</param>
        /// <param name="satelliteId">The NORAD CatId of the satellite the observation was for.</param>
        /// <param name="vettedStatus">The vetted status of the observation, for example "good", "bad", "failed", "unknown"</param>
        /// <param name="page">The page number. This is required because the result set is paginated. If null, returns the default page.</param>
        /// <returns></returns>
        [Get("/api/telemetry/?format=json&id={observationId}&satellite__norad_cat_id={satelliteId}&vetted_status=good&page={page}")]
        Task<List<ObservationEntry>> GetObservations(int? observationId = null, int? satelliteId = null,  string vettedStatus = null, int? page = null);
    }
}
