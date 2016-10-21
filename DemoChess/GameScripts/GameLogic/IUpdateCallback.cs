using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoChess.GameScripts.GameLogic
{
    interface IUpdateCallback
    {
        void UpdateBoardCallback(String boardstate);

    }


}