using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using DF.Contracts;
using DF.Core.Models;

namespace DF.EntityFramework
{
    public class Repository<TAggregate> : IRepository<TAggregate>
        where TAggregate : class, IAggregate, new()
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            this.Context = context;
        }

        protected DbSet<TAggregate> DbSet
        {
            get { return this.Context.Set<TAggregate>(); }
        }

        public IQueryable<TAggregate> Query
        {
            get { return this.DbSet.AsQueryable(); }
        }

        public IEnumerable<TAggregate> GetAllItems()
        {
            return this.Query;
        }


        public int GetItemCount()
        {
            return this.Query.Count();
        }

        public void Add(TAggregate aggregate)
        {
            this.DbSet.Add(aggregate);
        }

        public void Update(TAggregate aggregate)
        {
            var entry = this.Context.Entry(aggregate);

            if (entry.State == EntityState.Detached)
                this.DbSet.Attach(aggregate);

            entry.State = EntityState.Modified;
        }

        public void Remove(TAggregate aggregate)
        {
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