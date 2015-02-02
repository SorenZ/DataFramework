using System.Collections.Generic;
using System.Linq;

namespace DF.Core.Contracts
{
    /// <summary>
    /// Represents a generic repository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Gets the query.
        /// </summary>
        IQueryable<TEntity> Query { get; }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns> </returns>
        IEnumerable<TEntity> GetAllItems();

        /// <summary>
        /// Gets the item count.
        /// </summary>
        /// <returns> </returns>
        int GetItemCount();

        /// <summary>
        /// Adds the specified aggregate.
        /// </summary>
        /// <param name="entity"> The aggregate. </param>
        void Add(TEntity entity);

        /// <summary>
        /// Updates the specified Entity.
        /// </summary>
        /// <param name="aggregate"> The aggregate. </param>
        void Update(TEntity aggregate);

        /// <summary>
        /// Removes the specified aggregate.
        /// </summary>
        /// <param name="aggregate"> The Entity. </param>
        void Remove(TEntity aggregate);
    }
}