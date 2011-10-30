using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RockPaperScissorsPro;

namespace RockPaperAzure
{
    public static class MyBotExtensions
    {
        public static Move GetRandomDynamiteMove(this IPlayer you)
        {
            bool useDynamite = Moves.GetRandomNumber(2) == 1;
            if (useDynamite)
                return you.GetDynamiteMove();

            return Moves.GetRandomMove();
        }

        public static Move GetDynamiteMove(this IPlayer you)
        {
            if (you.HasDynamite)
                return Moves.Dynamite;

            return Moves.GetRandomMove();
        }
    }
}
