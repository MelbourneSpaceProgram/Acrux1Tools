using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace SatnogsApi
{
    public static class HttpClientFactoryExtensions
    {
        public static IServiceCollection AddSatnogsClients(this IServiceCollection services, Action<IHttpClientBuilder> dbSetup = null, Action<IHttpClientBuilder> networkSetup = null)
        {
            var dbBuilder = services.AddRefitClient<ISatnogsDbApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri("https://db.satnogs.org"));
            dbSetup?.Invoke(dbBuilder);

            var networkBuilder = services.AddRefitClient<ISatnogsNetworkApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri("https://network.satnogs.org"));
            networkSetup?.Invoke(networkBuilder);

            return services;
        }
    }
}
