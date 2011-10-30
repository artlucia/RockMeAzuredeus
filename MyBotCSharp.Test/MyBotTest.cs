using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperAzure;
using RockPaperScissorsPro;

namespace MyBotCSharp.Test
{
    /// <summary>
    ///This is a test class for MyBotTest and is intended
    ///to contain all MyBotTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MyBotTest
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
        ///A test for MakeMove
        ///</summary>
        [TestMethod()]
        public void MakeMoveTest()
        {
            MyBot target = new MyBot();
            Player testPlayer = new Player("Test", target);
            testPlayer.Reset(GameRules.Default);

            IPlayer opponent = new Player("Opponent", new MyBot());
            Move actual = target.MakeMove(testPlayer, opponent, GameRules.Default);
            Assert.IsNotNull(actual, "This test expected a move to be made.");
        }

        [TestMethod()]
        public void TestWhenSpecifyingCycleMode_ThenFirstMoveIsRock()
        {
            var target = new MyBot();
            target.Mode = MoveMode.Cycle;
            var testPlayer = new Player("Test", target);

            IPlayer opponent = new Player("Opponent", new MyBot());
            Move expected = Moves.Rock;
            Move actual = target.MakeMove(testPlayer, opponent, GameRules.Default);
            Assert.AreEqual(expected, actual, "Cycle always starts with Rock.");
        }

        [TestMethod()]
        public void TestWhenSpecifyingBigBangMode_ThenFirstMoveIsDynamite()
        {
            var target = new MyBot();
            target.Mode = MoveMode.BigBang;
            var testPlayer = new Player("Test", target);

            IPlayer opponent = new Player("Opponent", new MyBot());
            Move expected = Moves.Dynamite;
            Move actual = target.MakeMove(testPlayer, opponent, GameRules.Default);
            Assert.AreEqual(expected, actual, "Big Bang always starts with Dynamite.");
        }
    }
}
