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
            var entry = new TAggregate { Id = id };

            this.DbSet.Attach(entry);
            this.DbSet.Remove(entry);
        }
    }
}
