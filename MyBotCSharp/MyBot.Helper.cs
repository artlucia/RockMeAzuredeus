using System;
using System.Collections.Generic;
using System.Linq;
using RockPaperScissorsPro;

namespace RockPaperAzure
{
    public static class MyBotExtensions
    {
        public static Move GetRandomDynamiteMove(this IPlayer you)
        {
            bool useDynamite = Moves.GetRandomNumber(2) == 1;
            if (useDynamite)
                return Moves.Dynamite;

            return Moves.GetRandomMove();
        }

        public static Move GetDynamiteMove(this IPlayer you)
        {
            if (you.HasDynamite)
                return Moves.Dynamite;

            return Moves.GetRandomMove().GetWinningMove().GetWinningMove();
        }

        public static void LogError(this IPlayer you, Exception e, string message = null)
        {
            string logMessage = "ERROR";
            if (!String.IsNullOrWhiteSpace(message))
                logMessage = String.Concat(logMessage, " - ", message);
            logMessage += ": {0}";
            you.LogLine(logMessage, e);
        }

        public static void LogLine(this IPlayer you, string message, params object[] args)
        {
            if (you.Log == null) return;

            you.Log.AppendLine(String.Format(message, args));
        }

        public static Move GetWinningMove(this Move move)
        {
            if (move.Equals(Moves.Rock))
                return Moves.Paper;
            if (move.Equals(Moves.Paper))
                return Moves.Scissors;
            if (move.Equals(Moves.Scissors))
                return Moves.Rock;
            if (move.Equals(Moves.Dynamite))
                return Moves.WaterBalloon;
            return Moves.GetRandomMove();
        }

        public static string ToInitialString(this List<Move> list)
        {
            return String.Join("", list.Select(move => move.ToInitialString()));
        }

        public static string ToInitialString(this Move move)
        {
            if (move.Equals(Moves.Rock))
                return "R";
            if (move.Equals(Moves.Paper))
                return "P";
            if (move.Equals(Moves.Scissors))
                return "S";
            if (move.Equals(Moves.Dynamite))
                return "D";
            return "W";
        }
    }
}
