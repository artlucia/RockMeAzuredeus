using RockPaperAzure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Compete.Bot;

namespace MyBotCSharp.Test
{
    /// <summary>
    ///This is a test class for MyBotFactoryTest and is intended
    ///to contain all MyBotFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MyBotFactoryTest
    {
        #region TestContext and additional test attributes
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

        #endregion

        /// <summary>
        ///A test for CreateBot
        ///</summary>
        [TestMethod()]
        public void TestWhenCreatingBotFromFactory_ThenNoExceptionIsThrown()
        {
            MyBotFactory target = new MyBotFactory();
            IBot actual = target.CreateBot();
        }
    }
}
