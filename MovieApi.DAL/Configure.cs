using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieApi.Models;

namespace MovieApi.DAL
{
    public static class Configure
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            //Context lifetime defaults to "scoped"
            services
                 .AddDbContext<MovieDBContext>(options => options.UseSqlServer(connectionString));

            services
                .AddScoped<IRepository<Actor>, DALRepository<Actor>>()
                .AddScoped<IRepository<Producer>, DALRepository<Producer>>()
                .AddScoped<IRepository<Movie>, DALRepository<Movie>>()
                .AddScoped<IRepository<Person>, DALRepository<Person>>()
                .AddScoped<IRepository<RoleLookup>, DALRepository<RoleLookup>>()
                .AddScoped<IRepository<GenderLookup>, DALRepository<GenderLookup>>()
                .AddScoped<IRepository<CastAndCrew>, DALRepository<CastAndCrew>>();
        }
    }
}
