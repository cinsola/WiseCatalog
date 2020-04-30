using System.Collections.Generic;
using System.Linq;

namespace WiseCatalog.Data
{
    public interface IDataWrite<TEntity> : IEnumerable<TEntity>
    {
        IQueryable<TEntity> OriginalDbSet { get; set; }
        TEntity AddEntry(TEntity item);
    }
}
