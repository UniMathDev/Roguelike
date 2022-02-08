using Roguelike.Engine.Enums;
using System.Drawing;
using System.Collections.Generic;

namespace Roguelike.Engine
{
    public static class GameMath
    {
        public static Point DirectionToCoordDiff(Directions direction)
        {
            Dictionary<Directions, Point> DirectionToCoordDiff = new Dictionary<Directions, Point>
            {
                 {Directions.Up, new Point(0,-1)},
                 {Directions.RightUp, new Point(1,-1)},
                 {Directions.Right, new Point(1,0)},
                 {Directions.RightDown, new Point(1,1)},
                 {Directions.Down, new Point(0,1)},
                 {Directions.LeftDown, new Point(-1,1)},
                 {Directions.Left, new Point(-1,0)},
                 {Directions.LeftUp, new Point(-1,-1)},
            };
            return DirectionToCoordDiff[direction];
        }
        public static Directions CoordDiffToDirection(Point Diff)
        {
            Dictionary<Point, Directions> CoordDiffToDirection = new Dictionary<Point, Directions>
            {
                 {new Point(0,-1), Directions.Up},
                 {new Point(1,-1), Directions.RightUp },
                 {new Point(1,0), Directions.Right},
                 {new Point(1,1), Directions.RightDown },
                 {new Point(0,1), Directions.Down},
                 {new Point(-1,1), Directions.LeftDown},
                 {new Point(-1,0), Directions.Left},
                 {new Point(-1,-1), Directions.LeftUp }
            };
            return CoordDiffToDirection[Diff];
        }
    }
}
