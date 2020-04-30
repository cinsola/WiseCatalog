using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WiseCatalog.Data;
using WiseCatalog.Data.DTO;
using WiseCatalog.Data.Repository;
using WiseCatalog.Data.Schemas;

namespace WiseCatalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetValue<string>("Connection");
            ApplicationDbContextFactory.ConfigureServices(services, Configuration);
            services.AddSingleton<ApplicationDbContextFactory>(new ApplicationDbContextFactory());

            services.AddScoped<SurveyRepository>();
            _configureGraphQL(services);
            services.AddAuthentication();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        private void _configureGraphQL(IServiceCollection services)
        {
            services.AddScoped<IDocumentExecuter, ApplicationDocumentExecuter>();
            services.AddScoped<QuestionQuery>();
            services.AddScoped<QuestionMutation>();

            services.AddScoped<QuestionType>();
            services.AddScoped<SurveyType>();
            services.AddScoped<QuestionInputType>();

            var serviceProvider = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new ApplicationMutableSchema(new FuncDependencyResolver(type => serviceProvider.GetService(type))));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseGraphiQl();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
            app.UseSwaggerUi3();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
