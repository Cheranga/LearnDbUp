using System;
using DbUp;
using DbUpWebApi.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace DbUpWebApi.Filters
{
    public class DatabaseInitFilter : IStartupFilter
    {
        private readonly DatabaseConfig _config;
        private readonly DbLogger<DatabaseInitFilter> _logger;

        public DatabaseInitFilter(DatabaseConfig config, DbLogger<DatabaseInitFilter> logger)
        {
            _config = config;
            _logger = logger;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            var connectionString = _config.ConnectionString;

            EnsureDatabase.For.SqlDatabase(connectionString);

            var dbUpgradeEngineBuilder = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(typeof(Program).Assembly)
                .WithTransaction()
                .LogTo(_logger);

            var dbUpgradeEngine = dbUpgradeEngineBuilder.Build();
            if (dbUpgradeEngine.IsUpgradeRequired())
            {
                Console.WriteLine("Upgrades have been detected. Upgrading database now...");
                var operation = dbUpgradeEngine.PerformUpgrade();
                if (operation.Successful)
                {
                    _logger.WriteInformation("Upgrade completed successfully");
                }

                _logger.WriteInformation("Error happened in the upgrade. Please check the logs");
            }

            return next;
        }
    }
}