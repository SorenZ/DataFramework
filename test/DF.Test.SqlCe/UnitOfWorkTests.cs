using System;

using DF.Core.Contracts;
using DF.EntityFramework;
using DF.Test.SqlCe.DataModels;

using Xunit;

namespace DF.Test.SqlCe
{
    public class UnitOfWorkTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Post, Guid> _postRepository;
        private readonly IRepository<Blog, Guid> _blogRepository;

        public UnitOfWorkTests()
        {
            var context = InitDb.Init();

            this._unitOfWork = new UnitOfWork(context);
            this._postRepository = _unitOfWork.CreateRepository<Post, Guid>();
            this._blogRepository = _unitOfWork.CreateRepository<Blog, Guid>();
        }

        [Fact]
        public void Commit_Test()
        {
            var firstShotItemCount = this._blogRepository.GetItemCount();

            this._blogRepository.Add(new Blog
            {
                Name = "The Data Farm",
                Url = "http://thedatafarm.com/blog/"
            });

            this._unitOfWork.Commit();

            var secondShotItemCount = this._blogRepository.GetItemCount();

            Assert.True(secondShotItemCount > firstShotItemCount);
        }

        [Fact]
        public void RollBack_Test()
        {
            var firstShotItemCount = this._blogRepository.GetItemCount();

            this._blogRepository.Add(new Blog
            {
                Name = "The Data Farm",
                Url = "http://thedatafarm.com/blog/"
            });


            this._unitOfWork.RollBack();

            var secondShotItemCount = this._blogRepository.GetItemCount();

            Assert.True(secondShotItemCount == firstShotItemCount);
        }

    }
}
