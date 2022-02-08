using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;

namespace Roguelike.Engine
{
    public class Player : AnimatedObject
    {
        public Player(int x, int y) : base('@', x, y)
        {
            health = 100;
        }
    }
}
