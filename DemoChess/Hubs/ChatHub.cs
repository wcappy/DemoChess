using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using DemoChess.GameScripts;
using DemoChess.GameScripts.GameLogic.Pieces;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR;

namespace DemoChess.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly GameManager g = new GameManager();

        public void Send(String team, String name, String message)
        {
            if (!message.IsNullOrWhiteSpace())
            {
                if (message[0] == '#')
                {
                    string id = Context.ConnectionId;
                    g.HandleCommand(team, id, message);

                }
            }

            Clients.Others.broadcastMessage(team, name, message);
        }

        public void UpdateBoard()
        {
            String str = g.GetBoardState();
            if (str != null)
            {
                Clients.Caller.updateBoard(str);

            }
        }



    }
}