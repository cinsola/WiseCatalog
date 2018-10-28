using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiseCatalog.Data
{
    public static class ApplicationUsersInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationUserRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("test").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "test";
                user.Email = "test@localhost";
                user.FullName = "Utente 1";
                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "NormalUser").Wait();
                }
            }


            if (userManager.FindByNameAsync("admin").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@localhost";
                user.FullName = "Admin";

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<ApplicationUserRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("NormalUser").Result)
            {
                ApplicationUserRole role = new ApplicationUserRole
                {
                    Name = "NormalUser",
                    Description = "Utente dell'applicativo."
                };
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                ApplicationUserRole role = new ApplicationUserRole
                {
                    Name = "Administrator",
                    Description = "Amministratore."
                };
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
