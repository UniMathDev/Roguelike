using System;

namespace Roguelike.Engine.Monsters
{
    public class Monster : LivingObject
    {
        public Monster(int x, int y)
        {
            Character = 'Σ';
            ForegroundColor = ConsoleColor.DarkMagenta;
            Description = "Thing: A scary looking thing. ";
            Health = 100f;
            X = x;
            Y = y;
        }
    }
}

