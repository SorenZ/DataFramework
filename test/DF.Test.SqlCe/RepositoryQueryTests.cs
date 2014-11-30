using System;
using System.Linq;

using DF.Contracts;
using DF.EntityFramework;
using DF.Test.SqlCe.DataModels;

using Xunit;

namespace DF.Test.SqlCe
{
    public class RepositoryQueryTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Post, Guid> _repository;

        public RepositoryQueryTests()
        {
            var context = InitDb.Init();

            this._unitOfWork = new UnitOfWork(context);
            this._repository = _unitOfWork.CreateRepository<Post, Guid>();

        }

        [Fact]
        public void Query_Test()
        {
            var items = this._repository.Query
                .Where(q => q.Title.Contains(".NET"))
                .ToList();

            Assert.NotEmpty(items);
        }

        [Fact]
        public void GetAllItems_Test()
        {
            var items = this._repository.GetAllItems();

            Assert.Equal(3, items.Count());
        }

        [Fact]
        public void GetItemByKey_Test()
        {
            var key = this._repository.Query
                .First()
                .Id;

            var item = this._repository.GetItemByKey(key);

            Assert.NotNull(item);
        }

        [Fact]
        public void GetItemCount_Test()
        {
            Assert.Equal(3, this._repository.GetItemCount());
        }

    }
}
