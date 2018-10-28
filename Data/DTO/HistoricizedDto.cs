using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiseCatalog.Data.DTO
{
    public abstract class HistoricizedDto
    {
        public int Version { get; protected set; } = 0;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? LastModified { get; protected set; } = null;
        public bool IsDeleted { get; private set; } = false;
        public void Delete()
        {
            this.IsDeleted = true;
            this.LastModified = DateTime.UtcNow;
        }
    }

    public abstract class HistoricizedDtoType<T> : ObjectGraphType<T> where T: HistoricizedDto
    {
        public HistoricizedDtoType() {
            Field(x => x.Version);
            Field(x => x.IsDeleted);
            Field(x => x.CreatedAt);
            Field(x => x.LastModified, nullable: true);
        }
    }
}
