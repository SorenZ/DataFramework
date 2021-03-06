﻿using System.Data.Entity;

namespace DF.Test.SqlCe.DataModels
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(string connectionString) : base(connectionString)
        { } 

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Post> Posts { get; set; } 
    }
}
