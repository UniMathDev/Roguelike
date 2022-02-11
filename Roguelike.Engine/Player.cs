using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using System;

namespace Roguelike.Engine
{
    public class Player : LivingObject
    {
        public Player(int x, int y)
        {
            Character = '@';
            Description = "Me: thats me. ";
            health = 100;
            X = x;
            Y = y;
        }

    }
}
