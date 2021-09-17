using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAO;
using WebApplication1.Services;

namespace WebApplication1.ConfigureServices
{
    public static class IoC
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            services.AddScoped<PersonaDAO>();

            services.AddScoped<PersonaService>();

            return services;
        }
    }
}
