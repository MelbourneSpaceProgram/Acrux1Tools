using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatnogsApi
{
    public static class ISatnogsApiExtensions
    {
        public static async Task<List<TelemetryEntry>> GetAllTelemetry(this ISatnogsApi satnogsApi, int satelliteId, int maxPages = 256, int concurrentPages = 16)
        {
            List<TelemetryEntry> telemetry = new List<TelemetryEntry>();

            for (int i = 1; i < maxPages; i += concurrentPages)
            {
                var tasks = new List<Task<List<TelemetryEntry>>>();

                for (int j = 0; j < concurrentPages; j++)
                {
                    var task = satnogsApi.GetTelemetry(satelliteId, i + j);
                    tasks.Add(task);
                }

                bool end = false;

                foreach (var task in tasks)
                {
                    try
                    {
                        telemetry.AddRange(await task);
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

            return telemetry.GroupBy(t => t.Timestamp).Select(g => g.FirstOrDefault()).Where(t => t != null).ToList();
        }
    }
}
