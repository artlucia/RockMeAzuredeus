using RockPaperScissorsPro;
using System;

namespace RockPaperAzure
{
    public class MyBot : IRockPaperScissorsBot
    {
        // Random sample implementation
        public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
             return Moves.GetRandomMove();
        }

        // Cycle sample implementation
        //public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        //{
        //    if (you.LastMove == Moves.Rock)
        //        return Moves.Paper;

        //    if (you.LastMove == Moves.Paper)
        //        return Moves.Scissors;

        //    if (you.LastMove == Moves.Scissors)
        //        if (you.HasDynamite)
        //            return Moves.Dynamite;
        //        else
        //            return Moves.WaterBalloon;

        //    if (you.LastMove == Moves.Dynamite)
        //        return Moves.WaterBalloon;

        //    return Moves.Rock;
        //}

        // BigBang sample implementation
        //public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        //{
        //    if (you.NumberOfDecisions < 5)
        //        return Moves.Dynamite;
        //    else
        //        return Moves.GetRandomMove();
        //}
    }
}
