using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using WiseCatalog.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
                var userManager = (UserManager<ApplicationUser>)scope.ServiceProvider.GetService(typeof(UserManager<ApplicationUser>));
                var roleManager = (RoleManager<ApplicationUserRole>)scope.ServiceProvider.GetService(typeof(RoleManager<ApplicationUserRole>));
                var dbContext = (ApplicationDbContextFactory)scope.ServiceProvider.GetService(typeof(ApplicationDbContextFactory));
                using (var scopedContext = dbContext.CreateDbContext())
                {
                    //scopedContext.Database.EnsureCreated();
                    //scopedContext.EnsureSeedData(userManager, roleManager);
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
            return builder.ConfigureAppConfiguration(x => x.AddJsonFile("appsettings.Secrets.json"));
        }
    }
}
