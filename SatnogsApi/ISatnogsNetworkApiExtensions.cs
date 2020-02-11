using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SatnogsApi.Models.SatnogsNetwork;

namespace SatnogsApi
{
    public static class ISatnogsNetworkApiExtensions
    {
        public static async Task<List<ObservationEntry>> GetAllObservations(this ISatnogsNetworkApi api, int satelliteId, int maxPages = 1024, int concurrentPages = 64)
        {
            // There are two ways to retrieve all observations:
            //
            // * One at a time.
            //   After each observation is fetched, the "link" header can be checked for a rel= "next" link which will give the next page.
            //   This will give all pages until the last page is reached, and no rel= "next" link is provided.
            //   This is the "cleanest" method supported by the API.
            //
            // * Multithreaded
            //   Attempt to get an entire batch of observations, all simultaneously at once.
            //   Continue grabbing entire batches until we hit a 404 Not Found (or we could technically check for missing rel= "next" link but doesn't make much difference).
            //   This is fast and it works, but it's technically not a supported way to use the API.
            //
            // We use Multithreaded because it's so, so much quicker.
            // 

            List<ObservationEntry> observations = new List<ObservationEntry>();

            for (int i = 1; i < maxPages; i += concurrentPages)
            {
                var tasks = new List<Task<List<ObservationEntry>>>();

                for (int j = 0; j < concurrentPages; j++)
                {
                    var task = api.GetObservations(satelliteId, i + j);
                    tasks.Add(task);
                }

                bool end = false;

                foreach (var task in tasks)
                {
                    try
                    {
                        List<ObservationEntry> telemetryPage = await task;
                        observations.AddRange(telemetryPage);
                    }
                    catch (Exception ex) when (ex.Message.Contains("404"))
                    {
                        end = true;
                    }
                }

                if (end)
                {
                    break;
                }
            }

            // Remove duplicates from the observation list and return
            return observations.GroupBy(t => t.Id).Select(g => g.FirstOrDefault()).Where(t => t != null).ToList();
        }
    }
}
