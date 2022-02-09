using Roguelike.Engine.Enums;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System;

namespace Roguelike.Engine
{
    public static class GameMath
    {
        private static Dictionary<Direction, Point> DirectionToCoordDiffDictionary = new Dictionary<Direction, Point>
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
        public static Point DirectionToCoordDiff(Direction direction)
        {
            return DirectionToCoordDiffDictionary[direction];
        }

        private static Dictionary<Point, Direction> CoordDiffToDirectionDictionary = new Dictionary<Point, Direction>
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
        public static Direction CoordDiffToDirection(Point Diff)
        {

            return CoordDiffToDirectionDictionary[Diff];
        }
        public static string[] ChunksOf(string input, int chunkSize)
        {
            StringBuilder builder = new StringBuilder(input);
            List<string> list = new List<string>();
            for (int i = 0; i < input.Length; i += chunkSize)
            {
                list.Add(builder.ToString(i, Math.Min(input.Length - i - 1, chunkSize)));
            }
            return list.ToArray();
        }
    }
}
