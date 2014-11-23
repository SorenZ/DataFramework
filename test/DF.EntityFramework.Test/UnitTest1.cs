using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DF.EntityFramework.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            InitDb.SetupDatabase();
        }
    }
}
