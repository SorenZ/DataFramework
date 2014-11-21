using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using DF.Contracts;
using DF.Core.Models;
using DF.Core.Query;
using DF.EntityFramework.Extensions;

namespace DF.EntityFramework
{
    public class Repository<TAggregate> : IRepository<TAggregate>
        where TAggregate : class, IAggregate, new()
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            this._context = context;
        }

        public virtual DbSet<TAggregate> DbSet
        {
            get { return this._context.Set<TAggregate>(); }
        }

        public IQueryable<TAggregate> Query
        {
            get { return this.DbSet.AsQueryable(); }
        }

        public IEnumerable<TAggregate> GetAllItems()
        {
            return this.Query;
        }

        public IEnumerable<TAggregate> GetItems(int startIndex, int itemCount, SortDescription[] sortDescriptions = null)
        {
            return this.Query.ToPage(startIndex, itemCount, sortDescriptions);
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
            var entry = this._context.Entry(aggregate);

            if (entry.State == EntityState.Detached)
                this.DbSet.Attach(aggregate);

            entry.State = EntityState.Modified;
        }

        public void Remove(TAggregate aggregate)
        {
            var entry = this._context.Entry(aggregate);

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