using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this._repository = _unitOfWork.Repository<Post, Guid>();

        }

        [Fact]
        public void QueryTest()
        {
            var items = this._repository.Query
                .Where(q => q.Title.Contains(".NET"))
                .ToList();

            Assert.NotEmpty(items);
        }

        [Fact]
        public void GetAllItems()
        {
            var items = this._repository.GetAllItems();

            Assert.Equal(3, items.Count());
        }

        [Fact]
        public void GetItemByKey()
        {
            var key = this._repository.Query
                .First()
                .BlogId;

            var item = this._repository.GetItemByKey(key);

            Assert.NotNull(item);
        }

        [Fact]
        public void GetItemCountTest()
        {
            Assert.Equal(3, this._repository.GetItemCount());
        }

    }
}
