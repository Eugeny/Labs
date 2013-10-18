using Pankov.Lab1.RationalLab;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Pankov.Lab2.Tests
{


    /// <summary>
    ///This is a test class for RationalTest and is intended
    ///to contain all RationalTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RationalTest
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
        ///A test for CompareTo
        ///</summary>
        [TestMethod()]
        public void CompareToTest()
        {
            Rational target = new Rational(1, 3);
            Assert.AreEqual(target.CompareTo(new Rational(1, 2)), -1);
            Assert.AreEqual(target.CompareTo(new Rational(1, 3)), 0);
            Assert.AreEqual(target.CompareTo(new Rational(1, 4)), 1);
        }

        /// <summary>s
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            Rational x = null;
            Rational y = new Rational(1, 2);
            Assert.IsFalse(x == y);
            Assert.IsTrue(new Rational(1, 2) == new Rational(1, 2));
            Assert.IsFalse(new Rational(1, 3) == new Rational(1, 2));
            Assert.IsFalse(new Rational(1, 2) != new Rational(1, 2));
            Assert.IsTrue(new Rational(1, 3) != new Rational(1, 2));
            Assert.IsFalse(new Rational(1, 3).Equals(new DateTime()));
        }


        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            Assert.AreEqual<Rational>(new Rational(6, 5), Rational.Parse("1.2"));
            Assert.AreEqual<Rational>(new Rational(3, 100), Rational.Parse("0.03"));
            Assert.AreEqual<Rational>(new Rational(1, 30), Rational.Parse("0.0(3)"));
            Assert.AreEqual<Rational>(new Rational(2, 1), Rational.Parse("2"));
            Assert.AreEqual<Rational>(new Rational(3, 100), Rational.Parse("3/100"));
            try
            {
                Rational.Parse("0.03xgf");
                Assert.Fail();
            }
            catch { }
        }

        /// <summary>
        ///A test for Reduce
        ///</summary>
        [TestMethod()]
        public void ReduceTest()
        {
            Rational target = new Rational(4, 6);
            target = target.Reduce();
            Assert.AreEqual(target.Numerator, 2);
            Assert.AreEqual(target.Denominator, 3);
        }

        /// <summary>
        ///A test for ToDecimalString
        ///</summary>
        [TestMethod()]
        public void ToDecimalStringTest()
        {
            Assert.AreEqual("3", new Rational(6, 2).ToDecimalString());
            Assert.AreEqual("3.5", new Rational(7, 2).ToDecimalString());
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual("6/2", new Rational(6, 2).ToString());
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod()]
        public void op_AdditionTest()
        {
            Rational a = new Rational(1, 2);
            Rational b = new Rational(1, 3);
            Rational expected = new Rational(5, 6);
            Rational actual;
            actual = (a + b);
            Assert.AreEqual<Rational>(expected, actual);
        }

        /// <summary>
        ///A test for op_Implicit
        ///</summary>
        [TestMethod()]
        public void op_ImplicitTest()
        {
            double x = 1.2;
            Rational expected = new Rational(6, 5);
            Rational actual;
            actual = x;
            Assert.AreEqual<Rational>(expected, actual);
        }

        /// <summary>
        ///A test for op_Implicit
        ///</summary>
        [TestMethod()]
        public void op_ImplicitTest1()
        {
            int x = 2;
            Rational expected = new Rational(2, 1);
            Rational actual;
            actual = x;
            Assert.AreEqual<Rational>(expected, actual);
        }

        /// <summary>
        ///A test for op_Multiply
        ///</summary>
        [TestMethod()]
        public void op_MultiplyTest()
        {
            Rational a = new Rational(1, 2);
            Rational b = new Rational(2, 5);
            Rational expected = new Rational(1, 5);
            Rational actual;
            actual = (a * b);
            Assert.AreEqual<Rational>(expected, actual);
        }

        /// <summary>
        ///A test for op_Subtraction
        ///</summary>
        [TestMethod()]
        public void op_SubtractionTest()
        {
            Rational a = new Rational(1, 2);
            Rational b = new Rational(1, 3);
            Rational expected = new Rational(1, 6);
            Rational actual;
            actual = (a - b);
            Assert.AreEqual<Rational>(expected, actual);
        }

        /// <summary>
        ///A test for op_UnaryNegation
        ///</summary>
        [TestMethod()]
        public void op_UnaryNegationTest()
        {
            Rational x = new Rational(1, 2);
            Rational expected = new Rational(-1, 2);
            Rational actual;
            actual = -(x);
            Assert.AreEqual<Rational>(expected, actual);
        }


        /// <summary>
        ///A test for Rational Constructor
        ///</summary>
        [TestMethod()]
        public void RationalConstructorTest1()
        {
            Rational target = new Rational();
            Assert.AreEqual(target, new Rational(0, 1));
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest()
        {
            Assert.AreEqual(new Rational(1, 2).GetHashCode(), new Rational(1, 2).GetHashCode());
            Assert.AreNotEqual(new Rational(1, 2).GetHashCode(), new Rational(1, 1).GetHashCode());
        }

        /// <summary>
        ///A test for op_Division
        ///</summary>
        [TestMethod()]
        public void op_DivisionTest()
        {
            Rational a = new Rational(1, 2);
            Rational b = new Rational(2, 5);
            Rational expected = new Rational(5, 4);
            Rational actual;
            actual = (a / b);
            Assert.AreEqual<Rational>(expected, actual);
        }

        /// <summary>
        ///A test for op_GreaterThan
        ///</summary>
        [TestMethod()]
        public void op_GreaterThanTest()
        {
            Rational a = new Rational(1, 2);
            Rational b = new Rational(1, 3);
            bool expected = true;
            bool actual;
            actual = (a > b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_LessThan
        ///</summary>
        [TestMethod()]
        public void op_LessThanTest()
        {
            Rational a = new Rational(2, 3);
            Rational b = new Rational(1, 2);
            bool expected = false;
            bool actual;
            actual = (a < b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Implicit
        ///</summary>
        [TestMethod()]
        public void op_ImplicitTest2()
        {
            Rational x = new Rational(2, 5);
            double expected = 2.0 / 5;
            double actual;
            actual = x;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CreateAndVerify
        ///</summary>
        [TestMethod()]
        public void CreateAndVerifyTest()
        {
            try
            {
                Rational.CreateAndVerify(1, 0);
                Assert.Fail();
            }
            catch { }
            Rational.CreateAndVerify(1, 1);
        }
    }
}
