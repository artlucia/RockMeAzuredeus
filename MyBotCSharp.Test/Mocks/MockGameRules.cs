using System;
using RockPaperScissorsPro;

namespace MyBotCSharp.Test.Mocks
{
    public class MockGameRules: IDisposable
    {
        public Int32? MaximumGames { get; set; }
        public Int32? PointsToWin { get; set; }
        public Int32? StartingDynamite { get; set; }

        private Int32? OriginalMaximumGames = 0;
        private Int32? OriginalPointsToWin = 0;
        private Int32? OriginalStartingDynamite = 0;
        private GameRules OriginalRules = null;

        public GameRules GetGameRules()
        {
            return GetGameRules(GameRules.Default);
        }

        public GameRules GetGameRules(GameRules rulesToModify)
        {
            StoreOriginalRules(rulesToModify);
            GameRules rules = ModifyGameRules(rulesToModify);
            return rules;
        }

        private void StoreOriginalRules(GameRules rulesToModify)
        {
            this.OriginalMaximumGames = rulesToModify.MaximumGames;
            this.OriginalPointsToWin = rulesToModify.PointsToWin;
            this.OriginalStartingDynamite = rulesToModify.StartingDynamite;
            this.OriginalRules = rulesToModify;
        }

        private GameRules ModifyGameRules(GameRules gameRules)
        {
            var newRules = gameRules;
            foreach (var property in gameRules.GetType().GetProperties())
            {
                switch (property.Name)
                {
                    case "MaximumGames":
                        if (MaximumGames.HasValue)
                            property.SetValue(newRules, MaximumGames.Value, null);
                        break;
                    case "PointsToWin":
                        if (PointsToWin.HasValue)
                            property.SetValue(newRules, PointsToWin.Value, null);
                        break;
                    case "StartingDynamite":
                        if (StartingDynamite.HasValue)
                            property.SetValue(newRules, StartingDynamite.Value, null);
                        break;
                    default:
                        break;
                }
            }
            return newRules;
        }

        public void Dispose()
        {
            this.MaximumGames = this.OriginalMaximumGames;
            this.PointsToWin = this.OriginalPointsToWin;
            this.StartingDynamite = this.OriginalStartingDynamite;
            ModifyGameRules(this.OriginalRules);
        }
    }
}
