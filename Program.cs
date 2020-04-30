using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using WiseCatalog.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace WiseCatalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = (ApplicationDbContextFactory)scope.ServiceProvider.GetService(typeof(ApplicationDbContextFactory));
                using (var scopedContext = dbContext.CreateDbContext())
                {
                    scopedContext.EnsureSeedData(scope);
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            config.AddJsonFile("appsettings.secrets.json");
                        })
                .UseStartup<Startup>();
    }
}
