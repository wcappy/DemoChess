using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoChess.GameScripts.GameLogic.Pieces
{
    public class Rook : BasePiece
    {
        public override bool IsValid(Gameboard gameboard, int toX, int toY, int fromX, int fromY)
        {
            if (base.IsValid(gameboard, toX, toY, fromX, fromY) == false)
            {
                return false;
            }

            if (toY != fromY && toX != fromX)
            {
                if (toX == fromX)
                {
                    return true;
                }

                if (toY == fromY)
                { 
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            if (_PieceColor == PieceColor.BLACK)
            {
                return "BR";
            }

            return "WR";
        }
    }
}