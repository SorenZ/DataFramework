namespace DF.Core.Models
{
    /// <summary>
    /// Represent a collection of objects that are bound together by a root entity
    /// </summary>
    public interface IAggregate : IEntity
    { }

    /// <summary>
    /// Represent a collection of objects that are bound together by a root entity
    /// </summary>
    /// <typeparam name="TKey">Identity type of Aggregate</typeparam>
    public interface IAggregate<TKey> : IAggregate, IEntity<TKey>
    { }
}
