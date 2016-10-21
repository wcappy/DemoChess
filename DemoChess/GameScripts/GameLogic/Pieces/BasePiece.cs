using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoChess.GameScripts.GameLogic.Pieces
{
    public enum PieceColor
    {
        NONE,WHITE,BLACK
    }
    public class BasePiece
    {
        public bool Alive { get; set; }
        public PieceColor _PieceColor { get; set; }
        public virtual bool IsValid(Gameboard board ,int toX, int toY, int fromX, int fromY)
        {
            if (toX < 0 || toX > 7 || fromX < 0 || fromX > 7 || toY < 0 || toY > 7 || fromY < 0 || fromY > 7)
            {
                return false;
            }

            return true;
        }
    }
}