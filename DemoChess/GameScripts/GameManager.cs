using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using DemoChess.GameScripts.GameLogic;
using DemoChess.GameScripts.GameLogic.Pieces;
using DemoChess.GameScripts.SNS;
using DemoChess.Hubs;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR;

namespace DemoChess.GameScripts
{
    public class GameManager : IUpdateCallback
    {
        private GameDatabase database;
        public Game game;

        public GameModel dbModel { get; set; }
        private EmailUser emailUser;

        private Dictionary<String, int> votedMove;
        private IHubContext context;
        private bool canVote = true;

        private int gameTimerMax = 30;
        private int gametimer = 30;
        private int voteCounter = 0;
        private int displayCounter = 2;
        private static int RoundTimer = (1000) * 1;

        private readonly TimeSpan BroadcastInterval = TimeSpan.FromMilliseconds(RoundTimer);
        private Timer _broodcastLoop;


        public GameManager()
        {
            votedMove = new Dictionary<string, int>();
            database = new GameDatabase();
            context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            game = new Game(this);
            emailUser = new EmailUser();
            emailUser.Subscribe("ChessGame");
            // _broodcastLoop = new Timer(GameLoop,null,BroadcastInterval,BroadcastInterval);

            InitalizeGame();
         



        }


        public void GameLoop(object state)
        {
            context.Clients.All.roundTimer(gametimer);

            gametimer--;

            if (gametimer <= 0)
            {
                context.Clients.All.debugMessage("Calculating results voting now closed!");
                canVote = false;

                //get all votes
                //move move
                //save to database
                //update game boards
                //swap turn


                gametimer = gameTimerMax;
                canVote = true;
                context.Clients.All.debugMessage("voting now open!");

            }

        }


        public void HandleCommand(String color ,String clientId, String command)
        {
            Debug.WriteLine("START COMMAND");
            try
            {
                command = command.Remove(0, 1);
                command = command.ToLower();

                Debug.WriteLine(command);
                String[] split = command.Split(' ');

                if (canVote)
                {
                    if (split[0] == "move")
                    {
                        if (game.turnColor == PieceColor.BLACK && color == "Black")
                        {
                            if (split.Length == 2)
                            {
                                split = split[1].Split('-');
                                Debug.WriteLine("Move " + split[0] + " to " + split[1]);

                                if (game.CheckMove(split[0], split[1]) == false)
                                {
                                    context.Clients.Client(clientId).debugMessage("Invalid Move!");
                                }

                            }
                        }
                        else if (game.turnColor == PieceColor.WHITE && color == "White")
                        {
                            if (split.Length == 2)
                            {
                                split = split[1].Split('-');
                                Debug.WriteLine("Move " + split[0] + " to " + split[1]);

                                if (game.CheckMove(split[0], split[1]) == false)
                                {
                                    context.Clients.Client(clientId).debugMessage("Invalid Move!");
                                }

                            }
                        }
                    }
                    else if (split[0] == "nextturn" || split[0] == "endturn")
                    {
                        NextTurn();
                    }

                    else if (split[0] == "resetgame")
                    {
                        // RESET GAME
                        InitalizeGame();
                    }
                    else if (split[0] == "createdb")
                    {
                        database.CreateTable();
                        context.Clients.All.debugMessage("DB has been created!");


                    }
                    else if (split[0] == "getvotes")
                    {
                        GetAllVotes();
                    }
                    else if (split[0] == "savegame")
                    {
                        SaveGame();
                    }


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void InitalizeGame()
        {
            String gameId = Guid.NewGuid().ToString().Remove(4);
            Debug.WriteLine("GAME ID > " + gameId);

            dbModel = new GameModel();
            dbModel.GameId = gameId;
            dbModel.Time = DateTime.Now.Ticks;

            votedMove.Clear();
            GetAllVotes();
            game.turnColor = PieceColor.WHITE;
            context.Clients.All.turnIndicate("Whites Turn");

            game.ResetGame();

            RoundModel roundModel = new RoundModel();

            roundModel.BoardState = game.gameboard.ToString();
            roundModel.RoundNumber = 0;

            dbModel.Rounds.Add(roundModel);

        }

        private void SaveGame()
        {
            database.InsertEntry(dbModel);
            database.GetGameEntry(dbModel.GameId);
        }

        private void NextTurn()
        {

            try
            {
                string[] command = GetHighestVote().Split('-');
                game.MakeMove(command[0], command[1]);
                voteCounter = 0;
                RoundModel roundModel = new RoundModel();
                try
                {
                    foreach (var move in votedMove)
                    {
                        roundModel.MoveCounter.Add(move.Key,move.Value);
                    }
                }
                catch (Exception)
                {
                    
                }
             
                roundModel.BoardState = GetBoardState();
                roundModel.RoundNumber = game.GameRound;

                dbModel.Rounds.Add(roundModel);

                game.ChangeTeamTurn();
                if (game.turnColor == PieceColor.BLACK)
                {
                    context.Clients.All.turnIndicate("Blacks Turn");
                    emailUser.MessageUser("The White team has fished there turn nows its Blacks turn");

                }
                else
                {
                    context.Clients.All.turnIndicate("Whites Turn");
                    emailUser.MessageUser("The Black team has fished there turn nows its Whites turn");

                }
                votedMove.Clear();
                GetAllVotes();
            }
            catch (Exception)
            {
                
            }
       

        }

        public String GetHighestVote()
        {

            String highestMove = "";

            List<String> highestMoves = new List<String>();
            int highestCount = 0;
            foreach (var move in votedMove)
            {
                if (move.Value == highestCount)
                {
                    highestMoves.Add(move.Key);
                }
                else if (move.Value > highestCount)
                {
                    highestMoves.Clear();
                    highestMoves.Add(move.Key);
                }
                                
            }
            if (highestMoves.Count > 1)
            {
                Random random = new Random();
                int ran = random.Next(0, highestMoves.Count);
                highestMove = highestMoves[ran];
            }
            else
            {
                highestMove = highestMoves[0];
            }


            return highestMove;
        }

        public void AddVote(String str)
        {
            Debug.WriteLine("Add String " + str);
            if(!votedMove.ContainsKey(str))
                votedMove.Add(str,0);

            votedMove[str]++;
            voteCounter++;
            if (voteCounter >= displayCounter)
            {
                voteCounter = 0;
                GetAllVotes();
            }
        }

        public void GetAllVotes()
        {
            List<String> sendStrings = new List<string>();
            int i = 0;
            foreach (var move in votedMove)
            {
                sendStrings.Add(move.Key + " " + move.Value);
                Debug.WriteLine(move.Key+" "+move.Value);
                i++;
                if (i > 5)
                    break;
            }

            context.Clients.All.updateGraph(sendStrings);

        }

        public String GetBoardState()
        {
            if (!game.gameboard.ToString().IsNullOrWhiteSpace())
                return game.gameboard.ToString();

            return null;
        }

        public void UpdateBoardCallback(String boardstate)
        {
            
            context.Clients.All.updateBoard(boardstate);

        }
    }
}