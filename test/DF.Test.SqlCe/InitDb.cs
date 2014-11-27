using System;
using System.IO;

using DF.Test.SqlCe.DataModels;

namespace DF.Test.SqlCe
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
            var connectionString = "Datasource = " + filePath;

            using (var context = new BloggingContext(connectionString))
            {
                // this will create the database with the schema from the Entity Model
                context.Database.Create();
            }
        }
    }
}
