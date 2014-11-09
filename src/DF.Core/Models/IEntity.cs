namespace DF.Core.Models
{
    /// <summary>
    /// An object that is not defined by its attributes, but rather by a thread of continuity and its identity.
    /// </summary>
    /// <see href="http://en.wikipedia.org/wiki/Entity"/>
    public interface IEntity
    { }

    /// <summary>
    /// An object that is not defined by its attributes, but rather by a thread of continuity and its identity.
    /// </summary>
    /// <typeparam name="TKey">Identity Type of Entity</typeparam>
    public interface IEntity<TKey> : IEntity
    {
        /// <summary>
        /// describes the property of objects that distinguishes them from other objects.
        /// </summary>
        TKey Id { get; set; }
    }
}
