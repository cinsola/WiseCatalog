using GraphQL.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WiseCatalog.Data.SqlServer;

namespace WiseCatalog.Data
{
    public class ApplicationDbContextFactory
    {
        static private Func<IDataContext> ResolveCreateDbContext;
        public IDataContext CreateDbContext(string[] args)
        {
            return ResolveCreateDbContext();
        }

        public IDataContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public T InvokeSafe<T>(Func<IDataContext, T> action)
        {
            T result = default(T);
            var s = CreateDbContext();
            result = action(s);
            return result;
        }

        internal static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var dbApiType = Type.GetType(configuration["api"]);
            ResolveCreateDbContext = (Func<IDataContext>)dbApiType.GetMethod("ConfigureServices").Invoke(null, new object[] { services, configuration });
        }
    }
}