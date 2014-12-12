using System;
using System.Linq;

using DF.Core.Contracts;
using DF.EntityFramework;
using DF.Test.SqlCe.DataModels;

using Xunit;

namespace DF.Test.SqlCe
{
    public class RepositoryCommandTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Post, Guid> _postRepository;
        private readonly IRepository<Blog, Guid> _blogRepository;

        public RepositoryCommandTests()
        {
            var context = InitDb.Init();

            this._unitOfWork = new UnitOfWork(context);
            this._postRepository = _unitOfWork.CreateRepository<Post, Guid>();
            this._blogRepository = _unitOfWork.CreateRepository<Blog, Guid>();

        }

        [Fact]
        public void Add_Test()
        {
            var key = Guid.NewGuid();

            this._blogRepository.Add(new Blog
            { 
                Id = key,
                Name = "The Data Farm",
                Url = "http://thedatafarm.com/blog/"
            });

            this._unitOfWork.Commit();

            var item = this._blogRepository.GetItemByKey(key);

            Assert.NotNull(item);
        }

        [Fact]
        public void Update_Test()
        {
            var item = this._blogRepository.Query
                .First();

            var newName = item.Name + "Updated!";
            item.Name = newName;

            this._blogRepository.Update(item);

            this._unitOfWork.Commit();

            var updatedItem = this._blogRepository.GetItemByKey(item.Id);

            Assert.Equal(updatedItem.Name, newName);
        }

        [Fact]
        public void Remove_Test()
        {
            var item = this._postRepository.Query
                .First();

            var itemKey = item.Id;

            this._postRepository.Remove(item);
            this._unitOfWork.Commit();

            var removedItem = this._postRepository
                .GetItemByKey(itemKey);

            Assert.Null(removedItem);

        }

        [Fact]
        public void DeleteItemByKey_Test()
        {
            var item = this._postRepository.Query
                .First();

            var itemKey = item.Id;

            this._postRepository.DeleteItemByKey(itemKey);
            this._unitOfWork.Commit();

            var removedItem = this._postRepository
                .GetItemByKey(itemKey);

            Assert.Null(removedItem);
        }

    }
}
