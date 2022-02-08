using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using System.Drawing;

namespace Roguelike.Engine
{
    public abstract class AnimatedObject
    {
        public char Character { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public Point coordinates
        {
            get
            {
                return new Point(X, Y);
            }
        }
        public float health { get; set; }

        public AnimatedObject(char character, int x, int y)
        {
            Character = character;
            X = x;
            Y = y;
        }

        public bool CanMove(Directions direction, Map map)
        {
            System.Drawing.Point coordDiff = GameMath.DirectionToCoordDiff(direction);
            return map.IsPossibleToMove(X + coordDiff.X, Y + coordDiff.Y);
        }

        public void Damage(float amount)
        {
            health -= amount;
        }
    }
}

