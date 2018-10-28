using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiseCatalog.Data.DTO;

namespace WiseCatalog.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, string>
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("User");
            builder.Entity<ApplicationUserRole>().ToTable("Role");
            base.OnModelCreating(builder);
        }

        internal void EnsureSeedData(UserManager<ApplicationUser> _userManager, RoleManager<ApplicationUserRole> _roleManager)
        {
            ApplicationUsersInitializer.SeedData(_userManager, _roleManager);
            var user = Users.First(x => x.Email == "test@localhost");
            Survey defaultSurvey = new Survey
            {
                ApplicationUser = user,
                Name = "Libro",
                Questions = new List<Question>(

                        new Question[]
                        {
                             new Question { Name = "Titolo del libro" },
                             new Question { Name = "Numero di pagine" }
                        }
                    )
            };
            if (this.Surveys.Any() == false)
            {
                this.Surveys.Add(defaultSurvey);
                this.SaveChanges();
            }
        }
    }
}