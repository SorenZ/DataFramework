using System.Linq;
using DF.Test.SqlCe.CustomiedRepository;

using Xunit;

namespace DF.Test.SqlCe
{
    public class CustomiedRepositoryTests
    {
        private readonly IPostRepository _postRepository;

        public CustomiedRepositoryTests()
        {
            var context = InitDb.Init();

            this._postRepository = new PostRepository(context);
        }

        [Fact]
        public void GetPostsByBlogId()
        {
            var blogId = this._postRepository
                .GetAllItems()
                .First()
                .BlogId;

            var items = this._postRepository.GetPostsByBlogId(blogId);

            Assert.Equal(3, items.Count());
        }
    }
}
