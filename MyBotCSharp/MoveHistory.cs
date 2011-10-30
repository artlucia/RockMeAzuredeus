using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RockPaperScissorsPro;

namespace RockPaperAzure
{
    internal class MoveHistory
    {
        internal List<Move> OpponentMoveHistory { get; private set; }
        internal List<Move> YourMoveHistory { get; private set; }

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
        }
    }
}
