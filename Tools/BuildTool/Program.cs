using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BuildTool
{
    class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");
            var Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "\\appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{AppDomain.CurrentDomain.BaseDirectory}appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddUserSecrets("c86e8433-1aae-4540-b84e-d5121dc2a0cb")
                .Build();

            var myValue = Configuration.GetValue<string>("ConnectionStrings");

            Log.Logger = new LoggerConfiguration()
                  .ReadFrom.Configuration(Configuration)
                  .Enrich.FromLogContext()
                  .CreateLogger();

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging(config =>
                    {
                        config.ClearProviders();
                        config.AddProvider(new SerilogLoggerProvider(Log.Logger));
                    });
                });

            try
            {
                return await builder.RunCommandLineApplicationAsync<BuildToolCmd>(args);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return 1;
            }
        }

        static Task<string> GetHelloWorldAsync()
        {
            return Task.FromResult("Hello Async World");
        }
    }

    public class ConnectionStrings
    {
        public String DefaultConnection { get; set; }
        public String StorageConnectionString { get; set; }

        public ConnectionStrings() { }
    }
}
