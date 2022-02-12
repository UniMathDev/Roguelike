using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;

namespace Roguelike.Engine.Monsters
{
    public class Monster : LivingObject
    {
        public Monster(int x, int y)
        {
            Character = 'Σ';
            Description = "Thing: A scary looking thing. ";
            health = 100;
            X = x;
            Y = y;
        }
    }
}

