using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MovieApi.Services;

namespace MovieApi.Services
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddTransient<IActorService, ActorService>();
            services
                .AddTransient<IProducerService, ProducerService>();
        }
    }
}
