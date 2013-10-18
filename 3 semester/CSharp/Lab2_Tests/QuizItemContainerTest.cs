using Pankov.Lab2.Quiz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace Lab2_Tests
{


    /// <summary>
    ///This is a test class for QuizItemContainerTest and is intended
    ///to contain all QuizItemContainerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class QuizItemContainerTest
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
        ///A test for QuizItemContainer Constructor
        ///</summary>
        [TestMethod()]
        public void QuizItemContainerConstructorTest()
        {
            QuizItemContainer target = new QuizItemContainer();
            Assert.AreEqual(target.GetQuestionCount(), 0);
        }

        /// <summary>
        ///A test for GetCorrectAnswerCount
        ///</summary>
        [TestMethod()]
        public void GetCorrectAnswerCountTest()
        {
            QuizItemContainer target = Fixture.GetQuiz();
            ((Question)((QuizItemContainer)target[0])[0]).SelectAnswer(1);
            ((Question)target[1]).SelectAnswer(1);
            int expected = 1;
            int actual;
            actual = target.GetCorrectAnswerCount();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetQuestionCount
        ///</summary>
        [TestMethod()]
        public void GetQuestionCountTest()
        {
            QuizItemContainer target = Fixture.GetQuiz();
            int expected = 3;
            int actual;
            actual = target.GetQuestionCount();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsAnswered
        ///</summary>
        [TestMethod()]
        public void IsAnsweredTest()
        {
            QuizItemContainer target = Fixture.GetQuiz();
            bool expected = false;
            bool actual;
            actual = target.IsAnswered();
            Assert.AreEqual(expected, actual);
            ((Question)((QuizItemContainer)target[0])[0]).SelectAnswer(1);
            ((Question)((QuizItemContainer)target[0])[1]).SelectAnswer(1);
            ((Question)target[1]).SelectAnswer(1);
            actual = target.IsAnswered();
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///A test for IsAnsweredCorrectly
        ///</summary>
        [TestMethod()]
        public void IsAnsweredCorrectlyTest()
        {
            QuizItemContainer target = Fixture.GetQuiz();
            ((Question)((QuizItemContainer)target[0])[0]).SelectAnswer(1);
            ((Question)target[1]).SelectAnswer(1);
            Assert.IsFalse(target.IsAnsweredCorrectly());
            ((Question)((QuizItemContainer)target[0])[1]).SelectAnswer(1);
            ((Question)((QuizItemContainer)target[0])[1]).SelectAnswer(2);
            ((Question)target[1]).SelectAnswer(1);
            ((Question)target[1]).SelectAnswer(0);
            Assert.IsTrue(target.IsAnsweredCorrectly());
        }

        /// <summary>
        ///A test for System.Collections.IEnumerable.GetEnumerator
        ///</summary>
        [TestMethod()]
        public void GetEnumeratorTest()
        {
            IEnumerable target = Fixture.GetQuiz();
            IEnumerator actual = target.GetEnumerator();
            actual = target.GetEnumerator();
            actual.MoveNext();
            Assert.AreEqual(((Quiz)target)[0], actual.Current);
        }


        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            Assert.IsTrue(Fixture.GetQuiz() == Fixture.GetQuiz());
            Assert.IsFalse(Fixture.GetQuiz() != Fixture.GetQuiz());
            Assert.IsTrue(Fixture.GetQuiz().Equals(Fixture.GetQuiz()));
            Assert.IsFalse(Fixture.GetQuiz() == new Quiz());
            Assert.IsTrue(Fixture.GetQuiz() != new Quiz());
            Assert.IsFalse(Fixture.GetQuiz().Equals(new DateTime()));
            Assert.IsFalse(Fixture.GetQuiz().Equals(new Quiz()));
            Assert.IsFalse(Fixture.GetQuiz().Equals((Object)new Quiz()));
        }

    }
}
