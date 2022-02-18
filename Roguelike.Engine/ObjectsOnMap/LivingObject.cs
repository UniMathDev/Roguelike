using Roguelike.Engine.ObjectsOnMap;
using System;

namespace Roguelike.Engine
{
    public abstract class LivingObject : MobileObject
    {
        public float health { get; protected set; }
        public bool NextTo(int X, int Y)
        {
            if (Math.Abs(this.X - X) <= 1 && Math.Abs(this.Y - Y) <= 1)
            {
                return true;
            }
            return false;
        }
        public void Damage(float amount)
        {
            health -= amount;
        }
    }
}