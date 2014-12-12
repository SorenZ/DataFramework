using System.Data.Entity;
using System.Linq;

using DF.Core.Contracts;
using DF.Core.Models;

namespace DF.EntityFramework
{
    public class Repository<TAggregate,TKey> : Repository<TAggregate>,
        IRepository<TAggregate,TKey> where TAggregate : class, IAggregate<TKey>, new()
    {
        public Repository(DbContext context) : 
            base(context) { }

        public TAggregate GetItemByKey(TKey id)
        {
            return this.DbSet.Find(id);
        }

        public void DeleteItemByKey(TKey id)
        {
            var aggregate = new TAggregate { Id = id };

            // if item already was in DbSet local collection.
            var item = this.DbSet.Local.FirstOrDefault(e => Equals(e.Id, id));
            if (item != null)
            {
                this.DbSet.Remove(item);
                return;
            }

            var entry = this.Context.Entry(aggregate);

            if (entry.State == EntityState.Added)
            {
                entry.State = EntityState.Detached;
                return;
            }

            if (entry.State == EntityState.Detached)
                this.DbSet.Attach(aggregate);

            this.DbSet.Remove(aggregate);

        }
    }
}
