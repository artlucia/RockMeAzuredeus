using RockPaperScissorsPro;
using System;

namespace RockPaperAzure
{
    public class MyBot : IRockPaperScissorsBot
    {
        private MoveMode Mode = MoveMode.RockMeAzuredeus;
        private MoveHistory History = new MoveHistory();

        public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            Move resultingMove = null;

            switch (Mode)
            {
                case MoveMode.Random:
                    resultingMove = MakeRandomMove(you, opponent, rules);
                    break;
                case MoveMode.Cycle:
                    resultingMove = MakeCycleMove(you, opponent, rules);
                    break;
                case MoveMode.BigBang:
                    resultingMove = MakeBigBangMove(you, opponent, rules);
                    break;
                default:
                    resultingMove = MakeYourMove(you, opponent, rules);
                    break;
            }

            return resultingMove;
        }

        private Move MakeYourMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            History.StoreMoves(you, opponent);
            return you.GetRandomDynamiteMove();
        }

        // Random sample implementation
        private Move MakeRandomMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            return Moves.GetRandomMove();
        }

        // Cycle sample implementation
        private Move MakeCycleMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            if (you.LastMove == Moves.Rock)
                return Moves.Paper;

            if (you.LastMove == Moves.Paper)
                return Moves.Scissors;

            if (you.LastMove == Moves.Scissors)
                if (you.HasDynamite)
                    return Moves.Dynamite;
                else
                    return Moves.WaterBalloon;

            if (you.LastMove == Moves.Dynamite)
                return Moves.WaterBalloon;

            return Moves.Rock;
        }

        // BigBang sample implementation
        private Move MakeBigBangMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            if (you.NumberOfDecisions < 5)
                return Moves.Dynamite;
            else
                return Moves.GetRandomMove();
        }
    }
}
