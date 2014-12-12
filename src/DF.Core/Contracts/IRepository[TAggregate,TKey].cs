using DF.Core.Models;

namespace DF.Core.Contracts
{
    /// <summary>
    /// Represents a generic repository.
    /// </summary>
    public interface IRepository<TAggregate, in TKey> : IRepository<TAggregate> 
        where TAggregate : IAggregate<TKey>
    {
        /// <summary>
        /// Gets an item by its key.
        /// </summary>
        /// <returns> </returns>
        TAggregate GetItemByKey(TKey id);

        /// <summary>
        /// Delete the specific aggregate by Id (permanently)
        /// </summary>
        /// <param name="id"></param>
        void DeleteItemByKey(TKey id);
    }
}
