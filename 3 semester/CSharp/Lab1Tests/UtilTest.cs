using Pankov.Lab1.RationalLab;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Pankov.Lab2.Tests
{
    /// <summary>
    ///This is a test class for UtilTest and is intended
    ///to contain all UtilTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UtilTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GCD
        ///</summary>
        [TestMethod()]
        public void GCDTest()
        {
            int n = 120; 
            int d = 250; 
            int expected = 10;
            int actual;
            actual = Util.GCD(n, d);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GCF
        ///</summary>
        [TestMethod()]
        public void GCFTest()
        {
            int a = 6; 
            int b = 8; 
            int expected = 24;
            int actual;
            actual = Util.LCM(a, b);
            Assert.AreEqual(expected, actual);
        }
    }
}
