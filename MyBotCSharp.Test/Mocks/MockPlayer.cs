using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RockPaperScissorsPro;

namespace MyBotCSharp.Test.Mocks
{
    public class MockPlayer: IDisposable
    {
        public IGameLog Log { get; set; }

        private IGameLog OriginalLog = null;
        private Player OriginalPlayer = null;

        public Player GetPlayer(Player playerToModify)
        {
            StoreOriginalPlayer(playerToModify);
            Player player = ModifyPlayer(playerToModify);
            return player;
        }

        private void StoreOriginalPlayer(Player playerToModify)
        {
            this.OriginalLog = playerToModify.Log;
            this.OriginalPlayer = playerToModify;
        }

        private Player ModifyPlayer(Player playerToModify)
        {
            var newPlayer = playerToModify;
            foreach (var property in playerToModify.GetType().GetProperties())
            {
                switch (property.Name)
                {
                    case "Log":
                        property.SetValue(newPlayer, Log, null);
                        break;
                    default:
                        break;
                }
            }
            return newPlayer;
        }

        public void Dispose()
        {
            this.Log = this.OriginalLog;
            ModifyPlayer(this.OriginalPlayer);
        }
    }
}
