using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using TalkToApiStudyTest.Database;
using TalktoApiTest.TestProject.IntegrationTests.config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.TestHost;
using TalkToApiStudyTest;
using System;

namespace TalktoApiTest.TestProject.IntegrationTests.Controllers
{
    public class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<TalkToContext>));

                services.Remove(descriptor);
                services.RemoveAll(typeof(DbContextOptions<TalkToContext>));

                services.AddDbContext<TalkToContext>(options =>
                {
                    options.UseInMemoryDatabase("inMemoryDbForTesting");

                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<TalkToContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<Program>>>();

                    db.Database.EnsureDeleted();

                    db.Database.EnsureCreated();

                    try
                    {
                        DatabaseSeeder.Seed(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }


            }).ConfigureAppConfiguration((hostContext, configApp) =>
            {
                var env = hostContext.HostingEnvironment;
                configApp.AddJsonFile("appsettings.json", optional: true);
                configApp.AddEnvironmentVariables();

            });

        }

    }

}
 