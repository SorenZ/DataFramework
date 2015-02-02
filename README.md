DataFramework
=============
Sets of Models, Contracts, Patterns and Practices related to Data Driven Application.

How to Install 
---------------------------
Install [DF.EnittyFramework](https://www.nuget.org/packages/DF.EntityFramework/) from the package manager console:

    PM> Install-Package DF.EntityFramework

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
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        IRepository<TEntity, TKey> CreateRepository<TEntity, TKey>()
            where TEntity : class, IEntity<TKey>, new();

        /// <summary>
        /// provide basic repository for specific Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IRepository<TEntity> CreateRepository<TEntity>()
            where TEntity : class, IEntity, new();

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
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void Add(TAggregate entity);

        /// <summary>
        /// Updates the specified Aggregate.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void Update(TAggregate entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity"> The Aggregate. </param>
        void Remove(TAggregate entity);
    }
```

Samples
------------------
* Make generic Repository from UnitOfWork
```csharp
var unitOfWork = new UnitOfWork(context);
var blogRepository = _unitOfWork.CreateRepository<Blog, Guid>();

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
