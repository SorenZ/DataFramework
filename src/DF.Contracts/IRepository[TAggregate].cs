using System.Collections.Generic;
using System.Linq;

namespace DF.Contracts
{
    /// <summary>
    /// Represents a generic repository.
    /// </summary>
    /// <typeparam name="TAggregate"></typeparam>
    public interface IRepository<TAggregate>
    {
        /// <summary>
        /// Gets the query.
        /// </summary>
        IQueryable<TAggregate> Query { get; }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns> </returns>
        IEnumerable<TAggregate> GetAllItems();

        /// <summary>
        /// Gets the item count.
        /// </summary>
        /// <returns> </returns>
        int GetItemCount();

        /// <summary>
        /// Adds the specified aggregate.
        /// </summary>
        /// <param name="aggregate"> The aggregate. </param>
        void Add(TAggregate aggregate);

        /// <summary>
        /// Updates the specified Aggregate.
        /// </summary>
        /// <param name="aggregate"> The aggregate. </param>
        void Update(TAggregate aggregate);

        /// <summary>
        /// Removes the specified aggregate.
        /// </summary>
        /// <param name="aggregate"> The Aggregate. </param>
        void Remove(TAggregate aggregate);
    }
}