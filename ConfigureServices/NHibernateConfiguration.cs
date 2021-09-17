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
using NHibernate.Envers.Reader;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebApplication1.EnversNhibernete;

namespace WebApplication1.ConfigureServices
{
    public static class NHibernateConfiguration
    {
        //private static IConfiguration _configuration;
        // private static ISessionFactory _sessionFactory;

        public static IServiceCollection AddNHibernate(this IServiceCollection services, IConfiguration _configuration)
        {
            
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(NHibernateConfiguration).Assembly.ExportedTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();
            configuration.DataBaseIntegration(config =>
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
                //config.SchemaAction = SchemaAutoAction.Recreate; //si hay algun cambio recrea la base de datos, siempre la recrea y inserta
            });
            configuration.AddMapping(domainMapping);

            ///configurar envers para audit trail
           // var enversConf = new NHibernate.Envers.Configuration.Fluent.FluentConfiguration();

           // enversConf.Audit<Persona>();
       
            //enversConf.SetRevisionEntity<EnversRevisionEntity>(e => e.Id, e => e.RevisionDate, new EnversRevisionListener("prueba"));

            //var mets = enversConf.CreateMetaData(configuration);

            // EnversRevisionListener enversRevisionListener = new EnversRevisionListener("hola");
            //enversConf.SetRevisionEntity<EnversRevisionEntity>(e => e.Id, e => e.RevisionDate, enversRevisionListener);

 


            //EnversRevisionListener enversRevisionListener = new EnversRevisionListener("prueba");


           // configuration.IntegrateWithEnvers(enversConf);
            //configuration.SetProperty("nhibernate.envers.store_data_at_delete", "true");



            //var enversConf = new FluentConfiguration();
            //enversConf.Audit<Persona>();
            //nhConf.IntegrateWithEnvers(enversConf);


            var sessionFactory = configuration.BuildSessionFactory();

            ///////////esto del fluent//////////////
            //var _sessionFactory = Fluently.Configure()
            //                          .Database(PostgreSQLConfiguration.Standard.ConnectionString(connectionString))
            //                          .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
            //                          .BuildSessionFactory();


            //services.AddScoped(factory =>
            //{
            //    return _sessionFactory.OpenSession();
            //});

            //////////////////////fluent/////////////
            ///
            
            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());
            //services.AddTransient<IRevisionListener, EnversRevisionListener>();

            SetupEnvers(configuration);

            return services;
        }

        private static void SetupEnvers(NHibernate.Cfg.Configuration cfg)
        {
            //IServiceProvider services
            var enversConf = new NHibernate.Envers.Configuration.Fluent.FluentConfiguration();
            enversConf.Audit<Persona>();

            //IRevisionListener revListner = new EnversRevisionListener("prueba");

            //IRevisionListener revListner = services.GetService<IRevisionListener>();
            //enversConf.SetRevisionEntity<EnversRevisionEntity>(e => e.Id, e => e.RevisionDate, revListner);

            //cfg.SetEnversProperty(ConfigurationKey.AuditTableSuffix, "_LOG");
            //cfg.SetEnversProperty(ConfigurationKey.AuditStrategy, typeof(CustomValidityAuditStrategy));
            cfg.SetProperty("nhibernate.envers.store_data_at_delete", "true");
            cfg.IntegrateWithEnvers(enversConf);
        }
    } 
}
