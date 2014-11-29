using System;
using System.Data;

using DF.Core.Models;

namespace DF.Contracts
{
    /// <summary>
    /// Represents a unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// provide basic repository for specific Aggregate and Key
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        IRepository<TAggregate, TKey> CreateRepository<TAggregate, TKey>()
            where TAggregate : class, IAggregate<TKey>, new();

        /// <summary>
        /// provide basic repository for specific Aggregate
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        /// <returns></returns>
        IRepository<TAggregate> CreateRepository<TAggregate>()
            where TAggregate : class, IAggregate, new();

        /// <summary>
        /// Commits the works.
        /// </summary>
        void Commit();

        /// <summary>
        /// rolebacks the works.
        /// </summary>
        void RollBack();

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
    }
}
