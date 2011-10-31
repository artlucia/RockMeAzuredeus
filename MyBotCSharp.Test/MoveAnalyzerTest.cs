using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperAzure;
using RockPaperScissorsPro;

namespace MyBotCSharp.Test
{
    /// <summary>
    ///This is a test class for MoveAnalyzerTest and is intended
    ///to contain all MoveAnalyzerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MoveAnalyzerTest
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
        ///A test for Analyze
        ///</summary>
        [TestMethod()]
        public void TestWhenAnalysisHasNoHistory_ThenConfidenceIsVeryUnconfident()
        {
            var history = new MoveHistory_Accessor();
            var target = new MoveAnalyzer_Accessor(history);
            Player you = new Player("Test", new MyBot());
            you.Reset(GameRules.Default);

            target.Analyze(you);
            Confidence expected = Confidence.VeryUnconfident;
            Confidence actual = target.CurrentConfidence;
            Assert.AreEqual(expected, actual, "Odd that there is any confidence when there is no history.");
        }

        [TestMethod()]
        public void TestWhenAnalysisEncountersARepeatedMove_ThenConfidenceIsVeryConfidentAndBestGuessIsDetermined()
        {
            var history = new MoveHistory_Accessor();
            var target = new MoveAnalyzer_Accessor(history);
            Player you = new Player("Test", new MyBot());
            you.Reset(GameRules.Default);

            history.OpponentMoveHistory.Add(Moves.Dynamite);
            history.OpponentMoveHistory.Add(Moves.Dynamite);
            history.OpponentMoveHistory.Add(Moves.Dynamite);

            target.Analyze(you);
            Confidence expected = Confidence.VeryConfident;
            Confidence actual = target.CurrentConfidence;
            Assert.AreEqual(expected, actual, "Confidence after the DDD pattern is not as expected.");
            Move expectedMove = Moves.WaterBalloon;
            Move actualMove = target.BestGuess;
            Assert.AreEqual(expectedMove, actualMove, "Best guess after the DDD pattern is not as expected.");
        }
    }
}
