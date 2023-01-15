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
                services.AddTransient<DatabaseSeeder>();
            });

            builder.UseEnvironment("Development");

        }

    }

}
 