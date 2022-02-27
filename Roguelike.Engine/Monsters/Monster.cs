using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using System;

namespace Roguelike.Engine.Monsters
{
    public class Monster : LivingObject
    {
        public Monster(int x, int y, Map map, Action<LivingObject> OnDeath)
        {
            Character = 'Σ';
            ForegroundColor = ConsoleColor.DarkMagenta;
            Description = "Thing: A scary looking thing. ";
            Health = 100f;
            this.OnDeath += OnDeath.Invoke;
            map.SetObjWithCoord(x,y,this);
            X = x;
            Y = y;
        }
    }
}

