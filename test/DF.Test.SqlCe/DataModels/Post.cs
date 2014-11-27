using System;
using DF.Core.Models;

namespace DF.Test.SqlCe.DataModels
{
    public class Post : GuidAggregate
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid BlogId { get; set; }
        public virtual Blog Blog { get; set; } 
    }
}