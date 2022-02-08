using Roguelike.Engine.Enums;
using System.Drawing;
using System.Collections.Generic;

namespace Roguelike.Engine
{
    public static class GameMath
    {
        public static Point DirectionToCoordDiff(Direction direction)
        {
            Dictionary<Direction, Point> DirectionToCoordDiff = new Dictionary<Direction, Point>
            {
                 {Direction.Up, new Point(0,-1)},
                 {Direction.RightUp, new Point(1,-1)},
                 {Direction.Right, new Point(1,0)},
                 {Direction.RightDown, new Point(1,1)},
                 {Direction.Down, new Point(0,1)},
                 {Direction.LeftDown, new Point(-1,1)},
                 {Direction.Left, new Point(-1,0)},
                 {Direction.LeftUp, new Point(-1,-1)},
            };
            return DirectionToCoordDiff[direction];
        }
        public static Direction CoordDiffToDirection(Point Diff)
        {
            Dictionary<Point, Direction> CoordDiffToDirection = new Dictionary<Point, Direction>
            {
                 {new Point(0,-1), Direction.Up},
                 {new Point(1,-1), Direction.RightUp },
                 {new Point(1,0), Direction.Right},
                 {new Point(1,1), Direction.RightDown },
                 {new Point(0,1), Direction.Down},
                 {new Point(-1,1), Direction.LeftDown},
                 {new Point(-1,0), Direction.Left},
                 {new Point(-1,-1), Direction.LeftUp }
            };
            return CoordDiffToDirection[Diff];
        }
    }
}
