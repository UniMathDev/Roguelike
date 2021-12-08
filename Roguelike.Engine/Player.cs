using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;

namespace Roguelike.Engine
{
    public class Player
    {
        public char Character { get; }
        public int X { get; set; }
        public int Y { get; set; }

        public Player(int x, int y)
        {
            Character = '@';
            X = x;
            Y = y;
        }

        public bool CanMove(Directions direction, Map map)
        {
            switch (direction)
            {
                case Directions.Up:
                    return map.IsPossibleToMove(X, Y - 1);
                case Directions.Down:
                    return map.IsPossibleToMove(X, Y + 1);
                case Directions.Right:
                    return map.IsPossibleToMove(X + 1, Y);
                case Directions.Left:
                    return map.IsPossibleToMove(X - 1, Y);
                default:
                    return false;
            }
        }
    }
}
