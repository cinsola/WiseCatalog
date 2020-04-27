using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            Survey[] defaultSurveys = new Survey[] {
                new Survey {
                ApplicationUser = user,
                Name = "Book Survey",
                Questions = new List<Question>(

                        new Question[]
                        {
                             new Question { Name = "Can you tell me the book title?" },
                             new Question { Name = "Can you tell me the number of pages?" }
                        }
                    ) },
                new Survey {
                ApplicationUser = user,
                Name = "Film Survey",
                Questions = new List<Question>(
                    new Question[]
                    {
                        new Question { Name = "Can you tell me the movie title?" },
                        new Question { Name = "Can you tell me the movie duration?" }
                    })
                }
            };

            if (this.Surveys.Any() == false)
            {
                this.Surveys.AddRange(defaultSurveys);
                this.SaveChanges();
            }
        }
    }
}