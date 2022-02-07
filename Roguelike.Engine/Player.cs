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
            System.Drawing.Point coordDiff = GameMath.DirectionToCoordDiff(direction);
            return map.IsPossibleToMove(X + coordDiff.X, Y + coordDiff.Y);
        }
    }
}
