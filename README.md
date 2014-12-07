DataFramework
=============
Sets of Models, Contracts, Patterns and Practices related to Data Driven Application.

Repository and Unit of Work
---------------------------

```csharp
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
    }
```
```csharp
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
```

Samples
------------------
* Make generic Repository from UnitOfWork
```csharp
unitOfWork = new UnitOfWork(context);
blogRepository = _unitOfWork.CreateRepository<Blog, Guid>();

blogRepository.Add(new Blog
  { 
    Id = key,
    Name = "The Data Farm",
    Url = "http://thedatafarm.com/blog/"
  });
  
unitOfWork.Commit();
```
* Delete an item by key
```csharp
  var item = postRepository.Query
      .First();

  var itemKey = item.Id;

  postRepository.DeleteItemByKey(itemKey);
  unitOfWork.Commit();
```
