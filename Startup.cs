using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.ConfigureServices;
using WebApplication1.Middlewares;

namespace WebApplication1
{
    public class Startup
    {
        readonly string CorsConfiguration = "_corsConfigurations";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(conf =>
            {
                conf.AddPolicy(name: CorsConfiguration,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });



            //services.AddNHibernate(Configuration);
            services.AddFluentNHibernate(Configuration);

            services.AddRegistration();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();

            app.UseCors(CorsConfiguration);

            app.UseAuthorization();

            //Middleware - Inicio y fin de transaccion con BD
            //Middleware para las transacciones de la BD
            app.UseMiddleware<TransaccionDBMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
