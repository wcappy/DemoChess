using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoChess.GameScripts.GameLogic.Pieces;
using Microsoft.Ajax.Utilities;

namespace DemoChess.GameScripts.GameLogic
{
    public class Cell
    {

        public int x, y;
        public BasePiece piece;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool CheckMove(Gameboard gameboard,int toX,int toY)
        {
            if (piece == null)
                return false;
            if (piece.IsValid(gameboard, toX, toY, x, y))
            {
                return true;
            }
            return false;
        }


        public void SetPiece(BasePiece basePiece)
        {
            piece = basePiece;
        }

        public void SetPiece(String str)
        {
            if (str[0] == '#')
            {
                piece = null;
            }
            else
            {
                BasePiece basePiece = null;

                switch (str)
                {
                    case "BP":
                        basePiece = new Pawn();
                        basePiece._PieceColor = PieceColor.BLACK;
                        break;
                    case "BR":
                        basePiece = new Rook();
                        basePiece._PieceColor = PieceColor.BLACK;
                        break;
                    case "BK":
                        basePiece = new Knight();
                        basePiece._PieceColor = PieceColor.BLACK;
                        break;
                    case "BB":
                        basePiece = new Bishop();
                        basePiece._PieceColor = PieceColor.BLACK;
                        break;
                    case "BQ":
                        basePiece = new Queen();
                        basePiece._PieceColor = PieceColor.BLACK;
                        break;
                    case "BKI":
                        basePiece = new King();
                        basePiece._PieceColor = PieceColor.BLACK;
                        break;


                    case "WP":
                        basePiece = new Pawn();
                        basePiece._PieceColor = PieceColor.WHITE;
                        break;
                    case "WR":
                        basePiece = new Rook();
                        basePiece._PieceColor = PieceColor.WHITE;
                        break;
                    case "WK":
                        basePiece = new Knight();
                        basePiece._PieceColor = PieceColor.WHITE;
                        break;
                    case "WB":
                        basePiece = new Bishop();
                        basePiece._PieceColor = PieceColor.WHITE;
                        break;
                    case "WQ":
                        basePiece = new Queen();
                        basePiece._PieceColor = PieceColor.WHITE;
                        break;
                    case "WKI":
                        basePiece = new King();
                        basePiece._PieceColor = PieceColor.WHITE;
                        break;

                }
                piece = basePiece;
            }

        }

        public override string ToString()
        {
            if (piece == null)
            {
                return "#";
            }
            return piece.ToString();
        }



    }
}