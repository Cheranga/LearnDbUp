using System;
using DbUp;

namespace DbUpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Updating Database. Please stay tuned...");

            InitDatabase();
        }

        private static int InitDatabase()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlogDb;Integrated Security=True;Connect Timeout=30;";
            
            EnsureDatabase.For.SqlDatabase(connectionString);

            var dbUpgradeEngineBuilder = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(typeof(Program).Assembly)
                .WithTransaction()
                .LogToConsole();

            var dbUpgradeEngine = dbUpgradeEngineBuilder.Build();
            if (dbUpgradeEngine.IsUpgradeRequired())
            {
                Console.WriteLine("Upgrades have been detecred. Upgrading database now...");
                var operation = dbUpgradeEngine.PerformUpgrade();
                if (operation.Successful)
                {
                    Console.WriteLine("Upgrade completed successfully");
                    Console.ReadLine();

                    return 0;
                }

                Console.WriteLine("Error happened in the upgrade. Please check the logs");
                Console.ReadLine();

                return -1;
            }

            return 0;

        }
    }
}
