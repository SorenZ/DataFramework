using System;
using System.Data.Entity;

using DF.Core.Contracts;
using DF.Core.Models;

namespace DF.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext context)
        {
            this._context = context; 
        }

        private readonly DbContext _context;

        public IRepository<TAggregate, TKey> CreateRepository<TAggregate, TKey>() 
            where TAggregate : class, IAggregate<TKey>, new()
        {
            return new Repository<TAggregate, TKey>(this._context);
        }


        public IRepository<TAggregate> CreateRepository<TAggregate>() where TAggregate : class, IAggregate, new()
        {
            return new Repository<TAggregate>(this._context);
        }

        public void Commit()
        {
            this._context.SaveChanges();
        }

        public void RollBack()
        {
            foreach (var entity in this._context.ChangeTracker.Entries())
            {
                if (entity.State == EntityState.Added)
                {
                    entity.State = EntityState.Detached;
                }

                if (entity.State == EntityState.Deleted || entity.State == EntityState.Modified)
                {
                    entity.State = EntityState.Unchanged;
                }
            }
        }

        public void Dispose()
        {
            this._context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
