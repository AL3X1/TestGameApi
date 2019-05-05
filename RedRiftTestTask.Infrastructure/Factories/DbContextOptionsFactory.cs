using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql.Logging;

namespace RedRiftTestTask.Infrastructure.Factories
{
    public class DbContextOptionsFactory
    {
        private static string _connectionString;

        public static DbContextOptions<T> Get<T>() where T : DbContext
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                IConfigurationRoot configuration = BuildConfiguration();
                _connectionString = configuration.GetSection("connectionString").Value;
            }

            DbContextOptionsBuilder<T> optionsBuilder = new DbContextOptionsBuilder<T>();
            optionsBuilder.UseNpgsql(_connectionString);
            NpgsqlLogManager.Provider = new ConsoleLoggingProvider(NpgsqlLogLevel.Info, true, true);

            return optionsBuilder.Options;
        }
        
        private static IConfigurationRoot BuildConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();

            InitializeConfiguration();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("dbSettings.json", false, true); 

            return configurationBuilder.Build();
        }

        private static void InitializeConfiguration()
        {
            var path = $"{Directory.GetCurrentDirectory()}/dbSettings.json";

            if (!File.Exists(path))
            {
                var content = JsonConvert.SerializeObject(new Dictionary<string, string>()
                {
                    {
                        "connectionString", "Host=localhost;Port=5432;Database=RedRiftTestDb;Username=postgres;Password=123456"
                    }
                }, Formatting.Indented);
                File.WriteAllText(path, content);
            }
        }
    }
}