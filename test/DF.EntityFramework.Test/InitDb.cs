using System;
using System.IO;

using DF.EntityFramework.Test.DataModels;

namespace DF.EntityFramework.Test
{
    public class InitDb
    {

        public static void SetupDatabase()
        {
            // file path of the database to create
            var filePath = Environment.CurrentDirectory + @"\test.sdf";

            // delete it if it already exists
            if (File.Exists(filePath))
                File.Delete(filePath);

            // create the SQL CE connection string - this just points to the file path
            string connectionString = "Datasource = " + filePath;

            using (var context = new BloggingContext(connectionString))
            {
                // this will create the database with the schema from the Entity Model
                context.Database.Create();
            }
        }
    }
}
