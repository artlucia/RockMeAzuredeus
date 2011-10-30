using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperAzure;
using RockPaperScissorsPro;
using System;
using System.Threading.Tasks;

namespace MyBotCSharp.Test
{
    [TestClass()]
    public class GameTest
    {
        private const short GAMES_PER_MATCH = 15;

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

        [TestMethod()]
        public void TestWhenYouVsOpponent_ThenGameIsPlayed()
        {
            Player you = new Player("Test", new MyBot());
            Player opponent = new Player("Opponent", new MyBot());

            Game game = new Game(GameRules.Default);
            GameResults actual = game.Play(you, opponent);

            Assert.IsNotNull(actual.Winner, "This test expected a winner.");
        }

        [TestMethod()]
        public void TestWhenYouPlayRandomMoveOpponentManyTimes_ThenYouWinEverytime()
        {
            short expected = GAMES_PER_MATCH;
            short actual = PlaySomeOpponentManyTimes(MoveMode.Random, GAMES_PER_MATCH);

            Assert.AreEqual(expected, actual, "You did not win as much as expected.");
        }

        [TestMethod()]
        public void TestWhenYouPlayCycleOpponentManyTimes_ThenYouWinEverytime()
        {
            short expected = GAMES_PER_MATCH;
            short actual = PlaySomeOpponentManyTimes(MoveMode.Cycle, GAMES_PER_MATCH);

            Assert.AreEqual(expected, actual, "You did not win as much as expected.");
        }

        [TestMethod()]
        public void TestWhenYouPlayBigBangOpponentManyTimes_ThenYouWinEverytime()
        {
            short expected = GAMES_PER_MATCH;
            short actual = PlaySomeOpponentManyTimes(MoveMode.BigBang, GAMES_PER_MATCH);

            Assert.AreEqual(expected, actual, "You did not win as much as expected.");
        }

        private short PlaySomeOpponentManyTimes(MoveMode mode, short gameCount)
        {
            short wins = 0;

            Parallel.For(0, gameCount, i =>
                {
                    Player you = new Player("Test", new MyBot());
                    var accessor = new MyBot_Accessor();
                    accessor.Mode = mode;
                    Player opponent = new Player("Opponent", accessor);

                    Game game = new Game(GameRules.Default);
                    GameResults results = game.Play(you, opponent);

                    if (results.Winner.TeamName.Equals("Test")) wins++;
                });

            return wins;
        }
    }
}
