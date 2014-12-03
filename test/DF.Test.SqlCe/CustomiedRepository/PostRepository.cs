using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using DF.EntityFramework;
using DF.Test.SqlCe.DataModels;

namespace DF.Test.SqlCe.CustomiedRepository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DbContext context) : 
            base(context) { }


        public IEnumerable<Post> GetPostsByBlogId(Guid blogId)
        {
            return this.Query
                .Where(q => q.BlogId == blogId)
                .ToList();
        }
    }
}
