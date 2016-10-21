using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoChess.GameScripts.GameLogic.Pieces
{
    public class Bishop : BasePiece
    {

        public override bool IsValid(Gameboard gameboard, int toX, int toY, int fromX, int fromY)
        {
            if (base.IsValid(gameboard, toX, toY, fromX, fromY) == false)
            {
                return false;
            }

            if (Math.Abs(toX - fromX) == Math.Abs(toY - fromY))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            if (_PieceColor == PieceColor.BLACK)
            {
                return "BB";
            }

            return "WB";
        }
    }
}