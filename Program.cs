using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using WiseCatalog.Data;
using Microsoft.Extensions.DependencyInjection;
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
                var dbContext = (ApplicationDbContext)scope.ServiceProvider.GetService(typeof(ApplicationDbContext));
                dbContext.EnsureSeedData(userManager, roleManager);
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
