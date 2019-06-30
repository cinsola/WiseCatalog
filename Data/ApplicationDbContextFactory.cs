using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace WiseCatalog.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private string _connectionString;
        public ApplicationDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
        public ApplicationDbContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public T InvokeSafe<T>(Func<ApplicationDbContext, T> action)
        {
            T result = default(T);
            var s = CreateDbContext();
                result = action(s);
                return result;
        }

    }
}
