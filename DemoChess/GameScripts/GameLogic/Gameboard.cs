using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using DemoChess.GameScripts.GameLogic.Pieces;

namespace DemoChess.GameScripts.GameLogic
{
    public class Gameboard
    {
        public Dictionary<String, Cell> gameCells;
        public Cell[,] gameBoard;

        public Gameboard()
        {
           
            gameBoard = new Cell[8,8];
            gameCells = new Dictionary<string, Cell>();
            int x = 0;

            for (int i = 0; i < 8; i++)
            {
                for (char j = 'a'; j < 'i'; j++)
                {
                    Cell newCell = new Cell(i,x);
                    gameBoard[i, x] = newCell;
                    string id = j + "" + i;
                    gameCells.Add(id,newCell);
                    id = i + "" + j;
                    gameCells.Add(id, newCell);

                    x++;

                }
                x = 0;
            }

            foreach (var gameCell in gameCells)
            {
               Debug.WriteLine(gameCell.Key + " " + gameCell.Value); 
            }

        }

        public void SetGameBoard(String str)
        {
            String[] split = str.Split(' ');

            if (split.Length == 64)
            {
                int x = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        string set = split[x];
                        gameBoard[i,j].SetPiece(set);
                        x++;
                    }
                }

            }
            else
            {
                Debug.WriteLine("Input string invalid");
            }
        }


        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    output += gameBoard[i, j] + " ";
                }
            }
//            output = output.Remove(gameBoard.Length);


            return output;
        }
    }
}