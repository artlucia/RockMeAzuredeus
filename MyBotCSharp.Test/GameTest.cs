using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperAzure;
using RockPaperScissorsPro;

namespace MyBotCSharp.Test
{
    [TestClass()]
    public class GameTest
    {
        private const short GAMES_PER_MATCH = 50;

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
        public void TestWhenYouPlayRandomMoveOpponentManyTimes_ThenYouWinMostly()
        {
            short wins = PlaySomeOpponentManyTimes(MoveMode.Random, GAMES_PER_MATCH);

            double percentageWins = (double)wins / GAMES_PER_MATCH;
            double expected = 0.9;
            Assert.IsTrue(expected < percentageWins, String.Format("{0} >= {1}; You did not win as much as expected.", expected, percentageWins));
        }

        [TestMethod()]
        public void TestWhenYouPlayCycleOpponentManyTimes_ThenYouWinMostly()
        {
            short wins = PlaySomeOpponentManyTimes(MoveMode.Cycle, GAMES_PER_MATCH);

            double percentageWins = (double)wins / GAMES_PER_MATCH;
            double expected = 0.95;
            Assert.IsTrue(expected < percentageWins, String.Format("{0} >= {1}; You did not win as much as expected.", expected, percentageWins));
        }

        [TestMethod()]
        public void TestWhenYouPlayBigBangOpponentManyTimes_ThenYouWinMostly()
        {
            short wins = PlaySomeOpponentManyTimes(MoveMode.BigBang, GAMES_PER_MATCH);

            double percentageWins = (double)wins / GAMES_PER_MATCH;
            double expected = 0.9;
            Assert.IsTrue(expected < percentageWins, String.Format("{0} >= {1}; You did not win as much as expected.", expected, percentageWins));
        }

        private short PlaySomeOpponentManyTimes(MoveMode mode, short gameCount)
        {
            short wins = 0;
            var results = new List<GameResults>();

            Parallel.For(0, gameCount, i =>
                {
                    Player you = new Player("Test", new MyBot());
                    var accessor = new MyBot_Accessor();
                    accessor.Mode = mode;
                    Player opponent = new Player("Opponent", accessor);

                    Game game = new Game(GameRules.Default);
                    GameResults result = game.Play(you, opponent);

                    if (result.Winner.TeamName.Equals("Test")) wins++;
                    results.Add(result);
                });

            VerifyResults(results, String.Format("against {0}", mode));
            return wins;
        }

        private void VerifyResults(List<GameResults> results, string testDescription)
        {
            VerifyAllDynamiteUsed(results, testDescription);
            OutputLosses(results, testDescription);
        }

        private void VerifyAllDynamiteUsed(List<GameResults> results, string testDescription)
        {
            int expected = 0;
            int actual = results.Count(result => result.Player1.HasDynamite);

            Assert.AreEqual(expected, actual, String.Format("Games {0} had results with dynamite remaining.", testDescription));
        }

        private void OutputLosses(List<GameResults> results, string testDescription)
        {
            var losses = results.Where(result => result.Player1.Equals(result.Loser)).Select(result => result);
            foreach (var loss in losses)
            {
                Console.WriteLine("Loss result {0}", testDescription);
                Console.WriteLine("  Number of decisions: {0}", loss.Player1.NumberOfDecisions);
                Console.WriteLine("  Points: {0}", loss.Player1.Points);
                Console.WriteLine("  Total time deciding: {0}", loss.Player1.TotalTimeDeciding);
                Console.WriteLine("Log:");
                Console.WriteLine(loss.Player1Log);
                Console.WriteLine("  Last Moves: {0} and {1}", loss.Player1.LastMove, loss.Player2.LastMove);

                Console.WriteLine("");
                Console.WriteLine("");
            }
        }
    }
}
