using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApidotnetcore.Models;
using WebApidotnetcore.Models.data;

namespace WebApidotnetcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // Load the configuration
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                // Get the API endpoints configuration
                var apiEndpointsConfig = configuration.GetSection("ApiEndpoints").Get<List<ApiEndpointInfo>>();

                // Initialize the database context
                var dbContext = services.GetRequiredService<CollegeDbContext>();

                // Ensure the database is created
                dbContext.Database.EnsureCreated();

                // Seed the database with API endpoint data
                SeedApiEndpoints(dbContext, apiEndpointsConfig);

                // ... Rest of your code ...
            }

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
             
            });


        private static void SeedApiEndpoints(CollegeDbContext context, List<ApiEndpointInfo> apiEndpoints)
        {
            // Check if there are any existing records in the ApiEndpoints table
            if (!context.ApiEndpoints.Any())
            {
                // Seed the ApiEndpoints table with data
                foreach (var endpoint in apiEndpoints)
                {
                    context.ApiEndpoints.Add(endpoint);
                }

                context.SaveChanges();
                Console.WriteLine("Data seeded successfully.");
            }
            else
            {
                Console.WriteLine("ApiEndpoints table already contains data. No seeding required.");
            }
        }
    }
}
