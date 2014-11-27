using System;
using System.Collections.Generic;
using System.Data.Entity;
using DF.Test.SqlCe.DataModels;

namespace DF.Test.SqlCe
{
    public class InitDb
    {

        public static string ConnectionString
        {
            get { return string.Format("Datasource = {0}{1}", Environment.CurrentDirectory, @"\test.sdf"); }
        }

        public static DbContext Init()
        {
            SetupDatabase();
            InsertFakeData();

            return new BloggingContext(ConnectionString);
        }

        public static void SetupDatabase()
        {
            
            using (var context = new BloggingContext(ConnectionString))
            {
                context.Database.Delete();
                context.Database.Create();
            }
        }

        public static void InsertFakeData()
        {
            var context = new BloggingContext(ConnectionString);

            context.Blogs.Add(new Blog
            {
                Name = "HANSELMAN",
                Url = "http://www.hanselman.com/",
                Posts = new List<Post>
                {
                    new Post
                    {
                        Title = "ASP.NET 5 (vNext) Work in Progress",
                        Content = "TagHelpers are a new feature of ASP.NET 5 (formerly and colloquially ASP.NET vNext) but it's taken me (and others) some time to fully digest them and what they mean."
                    },
                    new Post
                    {
                        Title = "Announcing .NET 2015 - .NET as Open Source",
                        Content = "It's happening. It's the reason that a lot of us came to work for Microsoft, and I think it's both the end of an era but also the beginning of amazing things to come."
                    },
                    new Post
                    {
                        Title = "NuGet Package of the Week",
                        Content = "Yes, really. It's got to be the best name for an open source library out there. It's a great double entendre and a great name for this useful little library. Perhaps English isn't your first language, so I'll just say that a courtesy flush gives the next person a fresh bowl. ;)"
                    },
                }
            });

            context.SaveChanges();

        }

    }
}
