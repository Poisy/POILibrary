using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace POILibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(AddConfiguration) // Added by me
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        // POI: Creates settings and env variables
        public static void AddConfiguration(
            HostBuilderContext hostBuilder, 
            IConfigurationBuilder config)
        {
            config.AddEnvironmentVariables();
            config.AddJsonFile("appsettings.json");
        }
    }
}
