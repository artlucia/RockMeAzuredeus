using RockPaperAzure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RockPaperScissorsPro;

namespace MyBotCSharp.Test
{
    /// <summary>
    ///This is a test class for MoveHistoryTest and is intended
    ///to contain all MoveHistoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MoveHistoryTest
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
        ///A test for StoreMoves
        ///</summary>
        [TestMethod()]
        public void TestWhenStoringMovesBeforeMovesAreMade_ThenNothingIsStored()
        {
            var target = new MoveHistory_Accessor();
            IPlayer you = new Player("Test", new MyBot());
            IPlayer opponent = new Player("Opponent", new MyBot());

            target.StoreMoves(you, opponent);
            int expected = 0;
            int actual = target.OpponentMoveHistory.Count;
            Assert.AreEqual(expected, actual, "No opponent move should have been stored as no move was made yet.");
            actual = target.YourMoveHistory.Count;
            Assert.AreEqual(expected, actual, "No move for you should have been stored as no move was made yet.");
        }

        /// <summary>
        ///A test for StoreMoves
        ///</summary>
        [TestMethod()]
        public void TestWhenStoringMovesAfterMovesAreMade_ThenMovesAreStored()
        {
            var target = new MoveHistory_Accessor();
            Player you = new Player("Test", new MyBot());
            you.SetLastMove(new PlayerMove(you, Moves.Rock));
            Player opponent = new Player("Opponent", new MyBot());
            opponent.SetLastMove(new PlayerMove(opponent, Moves.WaterBalloon));

            target.StoreMoves(you, opponent);
            Move expected = Moves.WaterBalloon;
            Move actual = target.OpponentMoveHistory[0];
            Assert.AreEqual(expected, actual, "Opponent move was not saved as expected.");
            expected = Moves.Rock;
            actual = target.YourMoveHistory[0];
            Assert.AreEqual(expected, actual, "Your move was not saved as expected.");
        }
    }
}
