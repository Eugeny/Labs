using Pankov.Lab1.CurrencyLab;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Pankov.Lab1.Tests
{


    /// <summary>
    ///This is a test class for CurrencyTest and is intended
    ///to contain all CurrencyTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CurrencyTest
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
        ///A test for Currency Constructor
        ///</summary>
        [TestMethod()]
        public void CurrencyConstructorTest()
        {
            Symbols sym = Symbols.CAD; // TODO: Initialize to an appropriate value
            Decimal amount = 5M; // TODO: Initialize to an appropriate value
            Currency target = new Currency(sym, amount);
            Assert.AreEqual(target.Symbol, sym);
            Assert.AreEqual(target.Amount, amount);
        }

        /// <summary>
        ///A test for CompareTo
        ///</summary>
        [TestMethod()]
        public void CompareToTest()
        {
            Assert.AreEqual(new Currency(Symbols.EUR, 2).CompareTo(new Currency(Symbols.USD, 1)), 1);
            Assert.AreEqual(new Currency(Symbols.EUR, 2).CompareTo(new Currency(Symbols.USD, 4)), -1);
            Assert.AreEqual(new Currency(Symbols.EUR, 2).CompareTo(new Currency(Symbols.EUR, 2)), 0);
        }

        /// <summary>
        ///A test for Convert
        ///</summary>
        [TestMethod()]
        public void ConvertTest()
        {
            Symbols sym = Symbols.EUR;
            Decimal amount = 5M;
            Currency target = new Currency(sym, amount);
            Symbols to = Symbols.USD;
            Currency expected = new Currency(Symbols.USD, Market.GetConversionRatio(Symbols.EUR, Symbols.USD) * 5);
            Currency actual;
            actual = target.Convert(to);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            Assert.IsTrue(new Currency(Symbols.USD, 2).Equals(new Currency(Symbols.USD, 2)));
            Assert.IsFalse(new Currency(Symbols.USD, 3).Equals(new Currency(Symbols.USD, 2)));
            Assert.IsFalse(new Currency(Symbols.USD, 3).Equals(new DateTime()));
            Assert.IsTrue(new Currency(Symbols.USD, 2) == new Currency(Symbols.USD, 2));
            Assert.IsFalse(new Currency(Symbols.USD, 3) == new Currency(Symbols.USD, 2));
            Assert.IsFalse(new Currency(Symbols.USD, 3) == null);
            Assert.IsFalse(new Currency(Symbols.USD, 2) != new Currency(Symbols.USD, 2));
            Assert.IsTrue(new Currency(Symbols.USD, 3) != new Currency(Symbols.USD, 2));
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Symbols sym = Symbols.USD;
            Decimal amount = 2.2M;
            Currency target = new Currency(sym, amount);
            string expected = "USD 2.20";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod()]
        public void op_AdditionTest()
        {
            Currency a = new Currency(Symbols.USD, 2);
            Decimal b = 3M;
            Currency expected = new Currency(Symbols.USD, 5);
            Currency actual;
            actual = (a + b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod()]
        public void op_AdditionTest1()
        {
            Currency a = new Currency(Symbols.USD, 2);
            Currency b = new Currency(Symbols.USD, 5);
            Currency expected = new Currency(Symbols.USD, 7);
            Currency actual;
            actual = (a + b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Division
        ///</summary>
        [TestMethod()]
        public void op_DivisionTest()
        {
            Currency a = new Currency(Symbols.USD, 6);
            Decimal b = 3M;
            Currency expected = new Currency(Symbols.USD, 2);
            Currency actual;
            actual = (a / b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Multiply
        ///</summary>
        [TestMethod()]
        public void op_MultiplyTest()
        {
            Currency a = new Currency(Symbols.USD, 5);
            Decimal b = 6M;
            Currency expected = new Currency(Symbols.USD, 30);
            Currency actual;
            actual = (a * b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Subtraction
        ///</summary>
        [TestMethod()]
        public void op_SubtractionTest()
        {
            Currency a = new Currency(Symbols.USD, 7);
            Decimal b = 2;
            Currency expected = new Currency(Symbols.USD, 5);
            Currency actual;
            actual = (a - b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Subtraction
        ///</summary>
        [TestMethod()]
        public void op_SubtractionTest1()
        {
            Currency a = new Currency(Symbols.USD, 5);
            Currency b = new Currency(Symbols.USD, 2);
            Currency expected = new Currency(Symbols.USD, 3);
            Currency actual;
            actual = (a - b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            string s = "CAD 123";
            Currency expected = new Currency(Symbols.CAD, 123M);
            Currency actual;
            actual = Currency.Parse(s);
            Assert.AreEqual(expected, actual);

            try
            {
                Currency.Parse("ololo");
                Assert.Fail();
            }
            catch { }
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest()
        {
            Assert.AreNotEqual(new Currency(Symbols.CAD, 2).GetHashCode(), new Currency(Symbols.USD, 3).GetHashCode()); 
            Assert.AreEqual(new Currency(Symbols.CAD, 2).GetHashCode(), new Currency(Symbols.CAD, 2).GetHashCode());
        }
    }
}
