using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using System.Drawing;

namespace Roguelike.Engine
{
    public abstract class LivingObject
    {
        public char Character { get; protected set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Description { get; protected set; }
        public Point coordinates
        {
            get
            {
                return new Point(X, Y);
            }
        }
        public float health { get; protected set; }
        public bool CanMove(Direction direction, Map map)
        {
            Point coordDiff = GameMath.DirectionToCoordDiff(direction);
            return map.IsPossibleToMove(X + coordDiff.X, Y + coordDiff.Y);
        }

        public void Damage(float amount)
        {
            health -= amount;
        }
    }
}

