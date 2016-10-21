using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using DemoChess.GameScripts.GameLogic.Pieces;

namespace DemoChess.GameScripts.GameLogic
{
    public class Game
    {
        public int GameRound { get; set; }
        public Gameboard gameboard;
        public PieceColor turnColor;
        private readonly GameManager gm;

        public Game(GameManager gameManager)
        {
            gm = gameManager;
            gameboard = new Gameboard();
            turnColor = PieceColor.WHITE;
        }


        public void ChangeTeamTurn()
        {
            if (turnColor == PieceColor.WHITE)
            {
                turnColor = PieceColor.BLACK;
            }
            else
            {
                turnColor = PieceColor.WHITE;
            }
            GameRound++;
        }

        public bool CheckMove(String start , String end)
        {
            if (gameboard.gameCells[start] != null && gameboard.gameCells[end] != null)
            {
            
                Cell startCell, endCell;

                startCell = gameboard.gameCells[start];
                endCell = gameboard.gameCells[end];

                if (startCell.piece.IsValid(gameboard, endCell.x, endCell.y, startCell.x, startCell.y))
                {

                    gm.AddVote(start + "-" + end);
                    return true;

                }
            }
            
            return false;
        }

        public void MakeMove(String start, String end)
        {
            if (CheckMove(start, end))
            {
                Cell startCell, endCell;

                startCell = gameboard.gameCells[start];
                endCell = gameboard.gameCells[end];

                if (endCell.piece != null)
                {
                    endCell.SetPiece(startCell.piece);
                    startCell.SetPiece("#");
                }
                else
                {
                    endCell.SetPiece(startCell.piece);
                    startCell.SetPiece("#");
                    gm.UpdateBoardCallback(gameboard.ToString());
                }
            }
        }

        public void ResetGame()
        {
            string str = "BR BK BB BKI BQ BB BK BR " + "BP BP BP BP BP BP BP BP " + "# # # # # # # # " + "# # # # # # # # " + "# # # # # # # # " + "# # # # # # # # " + "WP WP WP WP WP WP WP WP " + "WR WK WB WKI WQ WB WK WR";
            gameboard.SetGameBoard(str);

            string positionStr = gameboard.ToString();
            gm.UpdateBoardCallback(positionStr);

        }


    }
}