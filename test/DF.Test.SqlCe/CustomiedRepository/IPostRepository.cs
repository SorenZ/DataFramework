using System;
using System.Collections.Generic;

using DF.Core.Contracts;
using DF.Test.SqlCe.DataModels;

namespace DF.Test.SqlCe.CustomiedRepository
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetPostsByBlogId(Guid blogId);
    }
}