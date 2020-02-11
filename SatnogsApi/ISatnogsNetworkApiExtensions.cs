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
        public static async Task<List<ObservationEntry>> GetAllObservations(this ISatnogsNetworkApi api, int satelliteId, IEnumerable<ObservationEntry> previousObservations = null, int maxPages = 1024, int concurrentPages = 64)
        {
            // There are two ways to retrieve all observations:
            //
            // * One at a time.
            //   After each observation is fetched, the "link" header can be checked for a rel= "next" link which will give the next page.
            //
            //   This will give all pages until the last page is reached, and no rel= "next" link is provided.
            //   This is the "cleanest" method supported by the API.
            //
            //
            // * Multithreaded
            //   Attempt to get an entire batch of observations, all simultaneously at once.
            //   Continue grabbing entire batches until we hit a 404 Not Found (or we could technically check for missing rel= "next" link but doesn't make much difference).
            //   We can also save a cache of previous Observation Ids, and then stop as soon as we hit one (since we've "caught up" to the latest data").
            //
            //   This is fast and it works, but it's technically not a supported way to use the API.
            //
            //
            // We use Multithreaded because it's so, so much quicker.
            //

            Dictionary<long, ObservationEntry> observations = new Dictionary<long, ObservationEntry>();
            HashSet<long> previousObservationIds = previousObservations == null ? null : new HashSet<long>(previousObservations.Select(oe => oe.Id));

            if (previousObservations != null)
            {
                foreach (var thisPreviousObservation in previousObservations)
                {
                    observations[thisPreviousObservation.Id] = thisPreviousObservation;
                }
            }

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
                        List<ObservationEntry> observationsPage = await task;
                        foreach (var thisObservation in observationsPage)
                        {
                            observations[thisObservation.Id] = thisObservation;
                        }

                        if (previousObservationIds != null)
                        {
                            // Check cache for observation.
                            // If we hit one, it indicates that we have "caught up" to the previous cache.
                            foreach (var observation in observationsPage)
                            {
                                if (previousObservationIds.Contains(observation.Id))
                                {
                                    end = true;
                                    break;
                                }
                            }
                        }
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

            return observations.Values.OrderBy(oe => oe.Id).ToList();
        }
    }
}
