using Pankov.Lab2.Quiz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace Lab2_Tests
{


    /// <summary>
    ///This is a test class for QuestionTest and is intended
    ///to contain all QuestionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class QuestionTest
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
        ///A test for Question Constructor
        ///</summary>
        [TestMethod()]
        public void QuestionConstructorTest()
        {
            Question target = new Question("asd", true);
            Assert.AreEqual(target.Text, "asd");
        }

        /// <summary>
        ///A test for GetCorrectAnswerCount
        ///</summary>
        [TestMethod()]
        public void GetCorrectAnswerCountTest()
        {
            Question target = Fixture.GetQuestion();
            target.SelectAnswer(2);
            target.SelectAnswer(1);
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
            Question target = Fixture.GetQuestion();
            int expected = 1; // TODO: Initialize to an appropriate value
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
            Question target = Fixture.GetQuestion();
            Assert.IsFalse(target.IsAnswered());
            target.SelectAnswer(0);
            Assert.IsTrue(target.IsAnswered());
        }

        /// <summary>
        ///A test for IsAnsweredCorrectly
        ///</summary>
        [TestMethod()]
        public void IsAnsweredCorrectlyTest()
        {
            Question target = Fixture.GetQuestion();
            target.SelectAnswer(1);
            Assert.IsFalse(target.IsAnsweredCorrectly());
            target.SelectAnswer(2);
            Assert.IsTrue(target.IsAnsweredCorrectly());
        }

        /// <summary>
        ///A test for SelectAnswer
        ///</summary>
        [TestMethod()]
        public void SelectAnswerTest()
        {
            Question target = Fixture.GetQuestion();
            target.SelectAnswer(1);
            Assert.IsTrue(target.Answers[1].Selected);
            target.SelectAnswer(target.Answers[2]);
            Assert.IsTrue(target.Answers[2].Selected);
        }

        /// <summary>
        ///A test for UnselectAnswer
        ///</summary>
        [TestMethod()]
        public void UnselectAnswerTest()
        {
            Question target = Fixture.GetQuestion();
            target.SelectAnswer(1);
            target.UnselectAnswer(1);
            Assert.IsFalse(target.Answers[1].Selected);
            target.SelectAnswer(target.Answers[2]);
            target.UnselectAnswer(target.Answers[2]);
            Assert.IsFalse(target.Answers[2].Selected);
        }


        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            Assert.IsTrue(Fixture.GetQuestion() == Fixture.GetQuestion());
            Assert.IsFalse(Fixture.GetQuestion() != Fixture.GetQuestion());
            Assert.IsTrue(Fixture.GetQuestion().Equals(Fixture.GetQuestion()));
            Assert.IsFalse(Fixture.GetQuestion() == new Question("asd", false));
            Assert.IsTrue(Fixture.GetQuestion() != new Question("asd", false));
            Assert.IsFalse(Fixture.GetQuestion().Equals(new DateTime()));
            Assert.IsFalse(Fixture.GetQuestion().Equals(new Question("asd", false)));
            Assert.IsFalse(Fixture.GetQuestion().Equals((Object)new Question("asd", false)));
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest()
        {
            Question t = Fixture.GetQuestion();
            Assert.AreEqual(t[0], t.Answers[0]);
        }

        /// <summary>
        ///A test for System.Collections.IEnumerable.GetEnumerator
        ///</summary>
        [TestMethod()]
        public void GetEnumeratorTest()
        {
            Question t = Fixture.GetQuestion();
            IEnumerator e = ((IEnumerable)t).GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, t[0]);
        }
    }
}
