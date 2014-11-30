using System.Data.Entity;

using DF.Contracts;
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
