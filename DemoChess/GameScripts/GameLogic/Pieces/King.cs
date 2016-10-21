using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoChess.GameScripts.GameLogic.Pieces
{
    public class King : BasePiece
    {
        public override bool IsValid(Gameboard gameboard, int toX, int toY, int fromX, int fromY)
        {
            if (base.IsValid(gameboard, toX, toY, fromX, fromY) == false)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            if (_PieceColor == PieceColor.BLACK)
            {
                return "BKI";
            }

            return "WKI";
        }
    }
}