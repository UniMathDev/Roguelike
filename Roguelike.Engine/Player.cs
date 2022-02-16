using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using Roguelike.Engine.Monsters;
using System.Collections.Generic;
using System;

namespace Roguelike.Engine
{
    public class Player : LivingObject
    {
        public Player(int x, int y)
        {
            Character = '@';
            ForegroundColor = ConsoleColor.White;
            Description = "Me: thats me. ";
            health = 100;
            X = x;
            Y = y;
        }
        public bool CanMove(Direction direction, Map map, List<Monster> monsters)
        {
            return base.CanMove(direction, map, monsters, this);
        }
    }
}
