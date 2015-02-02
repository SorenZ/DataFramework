using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using DF.Core.Contracts;
using DF.Core.Models;

namespace DF.EntityFramework
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            this.Context = context;
        }

        protected DbSet<TEntity> DbSet
        {
            get { return this.Context.Set<TEntity>(); }
        }

        public IQueryable<TEntity> Query
        {
            get { return this.DbSet.AsQueryable(); }
        }

        public IEnumerable<TEntity> GetAllItems()
        {
            return this.Query;
        }


        public int GetItemCount()
        {
            return this.Query.Count();
        }

        public void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
                this.DbSet.Attach(entity);

            entry.State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
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