using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using WiseCatalog.Data.DTO;

namespace WiseCatalog.Data.SqlServer
{
    public class SqlServerApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, string>, IDataContext
    {

        private DbSet<Question> Questions { get; set; }
        private DbSet<Survey> Surveys { get; set; }
        SqlServerDbSetLink<Question> _questionsLink;
        SqlServerDbSetLink<Survey> _surveysLink;

        public IDataWrite<Question> QuestionsReader => _questionsLink;

        public IDataWrite<Survey> SurveysReader => _surveysLink;


        public SqlServerApplicationDbContext(DbContextOptions<SqlServerApplicationDbContext> options) : base(options)
        {
            _questionsLink = new SqlServerDbSetLink<Question>(Questions);
            _surveysLink = new SqlServerDbSetLink<Survey>(Surveys);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("User");
            builder.Entity<ApplicationUserRole>().ToTable("Role");
            base.OnModelCreating(builder);
        }

        public void EnsureSeedData(IServiceScope scope)
        {
            var userManager = (UserManager<ApplicationUser>)scope.ServiceProvider.GetService(typeof(UserManager<ApplicationUser>));
            var roleManager = (RoleManager<ApplicationUserRole>)scope.ServiceProvider.GetService(typeof(RoleManager<ApplicationUserRole>));
            this.Database.EnsureCreated();
            ApplicationUsersInitializer.SeedData(userManager, roleManager);
        }

        public void EnsureSeedData()
        {
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

        public static Func<IDataContext> ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqlServerApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetValue<string>("Connection")), ServiceLifetime.Transient);
            services.AddIdentity<ApplicationUser, ApplicationUserRole>()
                .AddEntityFrameworkStores<SqlServerApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.User.RequireUniqueEmail = true;
            });

            return () =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<SqlServerApplicationDbContext>();
                optionsBuilder.UseSqlServer(configuration["connection"]);

                return new SqlServerApplicationDbContext(optionsBuilder.Options);
            };

        }
    }
}