using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
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

            try
            {
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
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

        //public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        //{
        //    throw new NotImplementedException();
        //}

        public void Dispose()
        {
            this._context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
