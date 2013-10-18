using Pankov.Lab2.Quiz.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Pankov.Lab2.Quiz;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Lab2_Tests
{


    /// <summary>
    ///This is a test class for XMLDocumentSerializerTest and is intended
    ///to contain all XMLDocumentSerializerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SerializerTest
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
        ///A test for Serialize
        ///</summary>
        [TestMethod()]
        public void SerializeTest()
        {
            Quiz orig = Fixture.GetQuiz();
            XDocument doc = new LinqXmlSerializer().Serialize(orig);
            Quiz des = new LinqXmlSerializer().Deserialize(doc);
            Assert.AreEqual<Quiz>(orig, des);
        }

        /// <summary>
        ///A test for DOMSerialize
        ///</summary>
        [TestMethod()]
        public void DOMSerializeTest()
        {
            Quiz orig = Fixture.GetQuiz();
            XmlDocument doc = new DomXmlSerializer().Serialize(orig);
            Quiz des = new DomXmlSerializer().Deserialize(doc);
            Assert.AreEqual<Quiz>(orig, des);
        }

        [TestMethod()]
        public void BinarySerializeTest()
        {
            Quiz orig = Fixture.GetQuiz();
            byte[] d = new BinarySerializer().Serialize(orig);
            Quiz des = new BinarySerializer().Deserialize(d);
            Assert.AreEqual<Quiz>(orig, des);
        }

        [TestMethod()]
        public void JsonSerializeTest()
        {
            Quiz orig = Fixture.GetQuiz();
            string d = new JsonSerializer().Serialize(orig);
            Quiz des = new JsonSerializer().Deserialize(d);
            Assert.AreEqual<Quiz>(orig, des);
        }

        [TestMethod()]
        public void SoapSerializeTest()
        {
            Quiz orig = Fixture.GetQuiz();
            string d = new SoapSerializer().Serialize(orig);
            Quiz des = new SoapSerializer().Deserialize(d);
            Assert.AreEqual<Quiz>(orig, des);
        }


        [TestMethod()]
        public void XmlSerializeTest()
        {
            Quiz orig = Fixture.GetQuiz();
            string d = new XmlSerializer().Serialize(orig);
            Quiz des = new XmlSerializer().Deserialize(d);
            Assert.AreEqual<Quiz>(orig, des);
        }
    }
}
