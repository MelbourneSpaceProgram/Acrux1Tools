using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace SatnogsApi
{
    public static class HttpClientFactoryExtensions
    {
        public static IHttpClientBuilder AddSatnogsClient(this IServiceCollection services)
        {
            return services.AddRefitClient<ISatnogsApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://db.satnogs.org"));
        }
    }
}
