using DF.Core.Models;

namespace DF.Core.Contracts
{
    /// <summary>
    /// Represents a generic repository.
    /// </summary>
    public interface IRepository<TEntity, in TKey> : IRepository<TEntity>
        where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Gets an item by its key.
        /// </summary>
        /// <returns> </returns>
        TEntity GetItemByKey(TKey id);

        /// <summary>
        /// Delete the specific aggregate by Id (permanently)
        /// </summary>
        /// <param name="id"></param>
        void DeleteItemByKey(TKey id);
    }
}
