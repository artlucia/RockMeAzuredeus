using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBotCSharp.Test.Mocks;
using RockPaperAzure;
using RockPaperScissorsPro;

namespace MyBotCSharp.Test
{
    /// <summary>
    ///This is a test class for MyBotExtensionsTest and is intended
    ///to contain all MyBotExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MyBotExtensionsTest
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
        ///A test for GetDynamiteMove
        ///</summary>
        [TestMethod()]
        public void TestWhenNoDynamiteRemaining_ThenGetDynamiteMoveDoesNotThrowDynamite()
        {
            using (var mock = new MockGameRules
            {
                MaximumGames = 1,
                PointsToWin = 1,
                StartingDynamite = 0
            })
            {
                Player you = new Player("Test", new MyBot());
                you.Reset(mock.GetGameRules(GameRules.Default));

                Move notExpected = Moves.Dynamite;
                Move actual = MyBotExtensions.GetDynamiteMove(you);
                Assert.AreNotEqual(notExpected, actual, "Should not have resulted in dynamite when there is no dynamite left.");
            }
        }

        /// <summary>
        ///A test for GetRandomDynamiteMove
        ///</summary>
        [TestMethod()]
        public void TestWhenReturningARandomDynamiteMove_ThenDynamiteIsReturnedSometimes()
        {
            Player you = new Player("Test", new MyBot());
            you.Reset(GameRules.Default);
            short numberOfTimesDynamiteWasReturned = 0;
            short maxAttempts = 100;

            for (short i = 0; i < maxAttempts; i++)
            {
                if (you.GetRandomDynamiteMove() == Moves.Dynamite)
                    numberOfTimesDynamiteWasReturned++;
            }

            Assert.IsTrue(numberOfTimesDynamiteWasReturned < maxAttempts, "Too many dynamites thrown, not very random.");
            Assert.IsTrue(numberOfTimesDynamiteWasReturned > 0, "No dynamites thrown, not very random.");
            //To see the following output, view the test result details.
            Console.WriteLine("Actual dynamite thrown out of {0} was {1}.", maxAttempts, numberOfTimesDynamiteWasReturned);
        }

        /// <summary>
        ///A test for ToInitialString
        ///</summary>
        [TestMethod()]
        public void ToInitialStringTest()
        {
            List<Move> list = new List<Move>();
            list.Add(Moves.WaterBalloon);
            list.Add(Moves.Scissors);
            list.Add(Moves.Rock);
            list.Add(Moves.Paper);
            list.Add(Moves.Paper);
            list.Add(Moves.Paper);
            list.Add(Moves.Dynamite);

            string expected = "WSRPPPD";
            string actual = list.ToInitialString();
            Assert.AreEqual(expected, actual, "ToInitialString is not setting output as expected.");
        }

        /// <summary>
        ///A test for LogLine
        ///</summary>
        [TestMethod()]
        public void TestWhenLoggingWithoutArgs_ThenLogIsWrittenToWithoutException()
        {
            var you = new Player("Test", new MyBot());
            using (var mock = new MockPlayer()
            {
                Log = new GameLog(you, new Player("Opponent", new MyBot()))
            })
            {
                var player = mock.GetPlayer(you);
                string message = "Test";
                MyBotExtensions.LogLine(player, message);
                Assert.IsNotNull(player.Log, "Could not have logged if the log object is not instantiated");
            }
        }

        /// <summary>
        ///A test for LogLine
        ///</summary>
        [TestMethod()]
        public void TestWhenLoggingWithArgs_ThenLogIsWrittenToWithoutException()
        {
            var you = new Player("Test", new MyBot());
            using (var mock = new MockPlayer()
            {
                Log = new GameLog(you, new Player("Opponent", new MyBot()))
            })
            {
                var player = mock.GetPlayer(you);
                string message = "Test {0} {1}";
                MyBotExtensions.LogLine(player, message, "1", "2");
                Assert.IsNotNull(player.Log, "Could not have logged if the log object is not instantiated");
            }
        }

        /// <summary>
        ///A test for LogError
        ///</summary>
        [TestMethod()]
        public void TestWhenLoggingErrorWithArgs_ThenLogIsWrittenToWithoutException()
        {
            var you = new Player("Test", new MyBot());
            using (var mock = new MockPlayer()
            {
                Log = new GameLog(you, new Player("Opponent", new MyBot()))
            })
            {
                var player = mock.GetPlayer(you);
                Exception e = new Exception("Test exception");
                string message = "Test";
                player.LogError(e, message);
                Assert.IsNotNull(player.Log, "Could not have logged if the log object is not instantiated");
            }
        }

        /// <summary>
        ///A test for GetWinningMove
        ///</summary>
        [TestMethod()]
        public void TestWhenCallingGetWinningMoveForWaterBalloon_ThenNeitherWaterBalloonNorDynamiteAreReturned()
        {
            Move move = Moves.WaterBalloon;
            Move notExpected = Moves.WaterBalloon;
            Move actual = MyBotExtensions.GetWinningMove(move);
            Assert.AreNotEqual(notExpected, actual, "GetWinningMove isn't treating WaterBalloon correctly.");
            notExpected = Moves.Dynamite;
            Assert.AreNotEqual(notExpected, actual, "GetWinningMove isn't treating WaterBalloon correctly.");
        }

        /// <summary>
        ///A test for GetWaterBalloonMove
        ///</summary>
        [TestMethod()]
        public void TestWhenCallingGetWaterBalloonMoveAgainstAnOpponentWithDynamite_ThenWaterBalloonIsReturned()
        {
            Player player = new Player("Opponent", new MyBot());
            player.Reset(GameRules.Default);
            Move expected = Moves.WaterBalloon;
            Move actual = MyBotExtensions.GetWaterBalloonMove(player);
            Assert.AreEqual(expected, actual, "There is dynamite to defend against.");
        }

        /// <summary>
        ///A test for GetWaterBalloonMove
        ///</summary>
        [TestMethod()]
        public void TestWhenCallingGetWaterBalloonMoveAgainstAnOpponentWithoutDynamite_ThenWaterBalloonIsNotReturned()
        {
            Player player = new Player("Opponent", new MyBot());
            Move notExpected = Moves.WaterBalloon;
            Move actual = MyBotExtensions.GetWaterBalloonMove(player);
            Assert.AreNotEqual(notExpected, actual, "There is no dynamite to defend against.");
        }
    }
}
