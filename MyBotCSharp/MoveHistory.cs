using System.Collections.Generic;
using RockPaperScissorsPro;

namespace RockPaperAzure
{
    internal class MoveHistory
    {
        internal List<Move> OpponentMoveHistory { get; private set; }
        internal List<Move> YourMoveHistory { get; private set; }
        internal int ConsecutiveTies { get; private set; }

        internal MoveHistory()
        {
            OpponentMoveHistory = new List<Move>();
            YourMoveHistory = new List<Move>();
        }

        internal void StoreMoves(IPlayer you, IPlayer opponent)
        {
            if (opponent.LastMove == null) return;

            OpponentMoveHistory.Add(opponent.LastMove);
            YourMoveHistory.Add(you.LastMove);

            if (opponent.LastMove.Equals(you.LastMove))
                ConsecutiveTies++;
            else
                ConsecutiveTies = 0;
        }
    }
}
