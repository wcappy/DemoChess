using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoChess.GameScripts.GameLogic.Pieces
{
    public class Knight : BasePiece
    {
        public override bool IsValid(Gameboard gameboard, int toX, int toY, int fromX, int fromY)
        {
            if (base.IsValid(gameboard, toX, toY, fromX, fromY) == false)
            {
                return false;
            }

            int x = Math.Abs(toX - fromX);
            int y = Math.Abs(toY - fromY);

            if (x * y == 2)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            if (_PieceColor == PieceColor.BLACK)
            {
                return "BK";
            }

            return "WK";
        }
    }
}