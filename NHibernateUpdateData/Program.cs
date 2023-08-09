using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernetCreateSession.Models;

namespace NHibernetCreateSession;
internal class Program
{
    public static void Main(string[] args)
    {
        using(var session = CreateSession())
        {
            using (var transaction = session.BeginTransaction())
            {
                var idToUpdate = 11; 
                var wallet = session.Get<Wallet>(idToUpdate);
                wallet.Balance = 999;

                session.Update(wallet);

                transaction.Commit();

                Console.WriteLine(wallet);
            }
        }
        Console.ReadKey();
    }


    private static ISession CreateSession()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetSection("ConnectionStrings:DigitalCurrencyDB").Value;

        var mapper = new ModelMapper();

        //list all of type mappings from assembly
        mapper.AddMappings(typeof(Wallet).Assembly.ExportedTypes);

        //Compile class mapping (so that from classes to XML) [Because we did it in FluentMapping]
        HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

        //Optional 
        //Console.WriteLine(domainMapping.AsString());


        //Allow the application to specify proprties and mapping documents 
        //to be used when creating 
        var hbConfig = new Configuration();
        //settings from app to NHibernate
        hbConfig.DataBaseIntegration(x =>
        {
            //Startegy to interact with the provider
            x.Driver<MicrosoftDataSqlClientDriver>();

            //dialect (like the version, for example in .NET7 we can't use the keyword record)
            //dialect => NHibernate uses to build syntax to rdbms

            x.Dialect<MsSql2012Dialect>();

            //Connection string 
            x.ConnectionString = connectionString;

            //Log SQL statements to console 
            //x.LogSqlInConsole = true;

            //Log Foramtted SQL 
            //x.LogFormattedSql = true;
        });
        //Add Mapping to NHibernate configuration
        hbConfig.AddMapping(domainMapping);

        //Instantiate a new ISessionFactory (That will use proprties, settings and mapping)
        var sessionFactory = hbConfig.BuildSessionFactory();
        var session = sessionFactory.OpenSession();

        return session;

    }

}


