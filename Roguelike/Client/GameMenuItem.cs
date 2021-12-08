using System;

namespace Roguelike.Client
{
    class GameMenuItem
    {
        public GameMenuItem(Action action)
        {
            Action = action;
        }

        public Action Action { get; set; }
    }
}
