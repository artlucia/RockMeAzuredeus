using System.Linq;
using RockPaperScissorsPro;

namespace RockPaperAzure
{
    internal class MoveAnalyzer
    {
        internal Confidence CurrentConfidence { get; private set; }
        internal Move BestGuess { get; private set; }
        private MoveHistory History = null;

        internal MoveAnalyzer(MoveHistory history)
        {
            this.History = history;
            ResetConfidence();
        }

        private void ResetConfidence()
        {
            CurrentConfidence = Confidence.VeryUnconfident;
            BestGuess = Moves.GetRandomMove();
        }

        internal void Analyze(IPlayer you)
        {
            var opponentMoves = History.OpponentMoveHistory;
            if (opponentMoves.Count < 3) return;

            if (opponentMoves.Count == 3)
            {
                var firstMove = opponentMoves.First();
                if (opponentMoves.All(move => move.Equals(firstMove)))
                {
                    CurrentConfidence = Confidence.VeryConfident;
                    BestGuess = firstMove.GetWinningMove();
                }
            }
            else
                ResetConfidence();
        }
    }
}
