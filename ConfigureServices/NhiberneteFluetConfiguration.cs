using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Envers;
using NHibernate.Envers.Configuration;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebApplication1.EnversNhibernete;

namespace WebApplication1.ConfigureServices
{
    public static class NhiberneteFluetConfiguration
    {
        public static IServiceCollection AddFluentNHibernate(this IServiceCollection services, IConfiguration _configuration)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            
            services.AddSingleton<ISessionFactory>(sp => Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard.ConnectionString(connectionString))
                  .Mappings(m => m.FluentMappings
                  .AddFromAssemblyOf<Program>())
                  .ExposeConfiguration(x =>
                  {   
                      x.DataBaseIntegration(config =>
                      {
                          config.ConnectionProvider<DriverConnectionProvider>();
                          config.Dialect<PostgreSQL83Dialect>();
                          config.Timeout = 60;
                          config.ConnectionString = connectionString;
                          config.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                          config.SchemaAction = SchemaAutoAction.Validate;
                          //config.LogFormattedSql = true;
                          //config.LogSqlInConsole = true;
                          config.Driver<NpgsqlDriver>();
                          config.SchemaAction = SchemaAutoAction.Update;
                      });
                  })
                  .ExposeConfiguration(config => SetupEnvers(config, sp))
                  //.ExposeConfiguration(config => new SchemaUpdate(config).Execute(false, true))
                  .BuildSessionFactory()
                 );

            services.AddScoped<ISession>(sp => sp.GetService<ISessionFactory>().OpenSession());
            services.AddTransient<IRevisionListener, EnversRevisionListener>();

            //FluentConfiguration fConfig = Fluently.Configure()
            //      .Database(PostgreSQLConfiguration.Standard.ConnectionString(connectionString))
            //      .Mappings(m => m.FluentMappings
            //      .AddFromAssemblyOf<Program>())
            //      .ExposeConfiguration(x =>
            //      {
            //          x.DataBaseIntegration(config =>
            //          {
            //              config.ConnectionProvider<DriverConnectionProvider>();
            //              config.Dialect<PostgreSQL83Dialect>();
            //              config.Timeout = 60;
            //              config.ConnectionString = connectionString;
            //              config.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
            //              config.SchemaAction = SchemaAutoAction.Validate;
            //              //config.LogFormattedSql = true;
            //              //config.LogSqlInConsole = true;
            //              config.Driver<NpgsqlDriver>();
            //              config.SchemaAction = SchemaAutoAction.Update;
            //          });
            //      })







            return services;
        }

        private static void SetupEnvers(NHibernate.Cfg.Configuration cfg, IServiceProvider services)
        {
            var enversConf = new NHibernate.Envers.Configuration.Fluent.FluentConfiguration();
            enversConf.Audit<Persona>();

            IRevisionListener revListner = services.GetService<IRevisionListener>();
            enversConf.SetRevisionEntity<EnversRevisionEntity>(e => e.Id, e => e.RevisionDate, revListner);

            cfg.IntegrateWithEnvers(enversConf);
        }
    }
}
