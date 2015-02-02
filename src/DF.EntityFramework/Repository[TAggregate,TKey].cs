using System.Data.Entity;
using System.Linq;

using DF.Core.Contracts;
using DF.Core.Models;

namespace DF.EntityFramework
{
    public class Repository<TEntity, TKey> : Repository<TEntity>,
        IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
    {
        public Repository(DbContext context) : 
            base(context) { }

        public TEntity GetItemByKey(TKey id)
        {
            return this.DbSet.Find(id);
        }

        public void DeleteItemByKey(TKey id)
        {
            var entity = new TEntity { Id = id };

            // if item already was in DbSet local collection.
            var item = this.DbSet.Local.FirstOrDefault(e => Equals(e.Id, id));
            if (item != null)
            {
                this.DbSet.Remove(item);
                return;
            }

            var entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Added)
            {
                entry.State = EntityState.Detached;
                return;
            }

            if (entry.State == EntityState.Detached)
                this.DbSet.Attach(entity);

            this.DbSet.Remove(entity);

        }
    }
}
