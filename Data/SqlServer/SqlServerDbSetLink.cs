using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WiseCatalog.Data.SqlServer
{
    public class SqlServerDbSetLink<T> : IDataWrite<T> where T : class
    {

        public IQueryable<T> OriginalDbSet { get; set; }

        public SqlServerDbSetLink(DbSet<T> originalDbSet)
        {
            OriginalDbSet = originalDbSet;
        }

        public T AddEntry(T item)
        {
            return (OriginalDbSet as DbSet<T>).Add(item).Entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return OriginalDbSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return OriginalDbSet.GetEnumerator();
        }
    }
}
