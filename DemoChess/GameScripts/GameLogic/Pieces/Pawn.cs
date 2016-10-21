using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoChess.GameScripts.GameLogic.Pieces
{
    public class Pawn : BasePiece
    {
        private bool hasMoved;

        public override bool IsValid(Gameboard gameboard, int toX, int toY, int fromX, int fromY)
        {
            if (base.IsValid(gameboard, toX, toY, fromX, fromY) == false)
            {
                return false;
            }

            //check check for side movement

            if (toY == fromY+1 || toY == fromY-1)
            {
                if (_PieceColor == PieceColor.BLACK)
                {
                    if (toX == fromX + 1)
                    {
                        if (gameboard.gameBoard[toX, toY].piece != null)
                        {
                            hasMoved = false;
                            return false;
                        }
                    }

                }
                else
                {
                    if (toX == fromX - 1)
                    {
                        if (gameboard.gameBoard[toX, toY].piece != null)
                        {
                            hasMoved = false;
                            return true;
                        }
                    }
                }
                
            }

            if (hasMoved)
            {
                if (_PieceColor == PieceColor.BLACK)
                {
                    if (toX == fromX + 1)
                    {
                        if (gameboard.gameBoard[toX, toY].piece == null)
                        {
                            return true;
                        }
                    }

                }
                else
                {
                    if (toX == fromX - 1)
                    {
                        if (gameboard.gameBoard[toX, toX].piece == null)
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                if (_PieceColor == PieceColor.BLACK)
                {
                    if (toX == fromX + 1|| toX == fromX + 2)
                    {
                        if (gameboard.gameBoard[toX, toY].piece == null)
                        {
                            hasMoved = false;
                            return true;
                        }
                    }

                }
                else
                {
                    if (toX == fromX - 1|| toX == fromX -2)
                    {
                        if (gameboard.gameBoard[toX, toY].piece == null)
                        {
                            hasMoved = false;
                            return true;
                        }
                    }
                }
            }


       

            return false;
        }



        public override string ToString()
        {
            if (_PieceColor == PieceColor.BLACK)
            {
                return "BP";
            }

            return "WP";
        }
    }
}