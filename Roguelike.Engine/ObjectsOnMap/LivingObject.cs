using Roguelike.Engine.ObjectsOnMap;
using System;

namespace Roguelike.Engine
{
    public abstract class LivingObject : MobileObject
    {

        public event OnDeathEvent OnDeath;
        public delegate void OnDeathEvent(LivingObject caller);
        private float health;
        public float Health
        {
            get
            {
                return health;
            }
            protected set
            {
                health = value;
                if (health <= 0f)
                {
                    OnDeath.Invoke(this);
                }
            }
        }
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
            Health -= amount;
        }
    }
}