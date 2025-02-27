﻿using Roguelike.Engine.Enums;
using Roguelike.GameConfig;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System;
using Roguelike.Engine.ObjectsOnMap;

namespace Roguelike.Engine
{
    public static class GameMath
    {
        public static Random rand = new Random(0);
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
                 {Direction.Null, new Point(0,0) }
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
                 {new Point(-1,-1), Direction.LeftUp },
                 {new Point(0,0),Direction.Null }
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
        public static string SizeDescription(int size)
        {
            switch (size)
            {
                case > PlayerStats.PocketSize: return "very big";
                case >=3: return "big";
                case 2: return "medium";
                case 1: return "small";
                default: throw new Exception("size has to be bigger than zero.");
            }
        }
        public static Direction[] allDirections =
        {
            Direction.LeftUp,
            Direction.Up,
            Direction.RightUp,
            Direction.Left,
            Direction.Right,
            Direction.LeftDown,
            Direction.Down,
            Direction.RightDown,
        };
    }
    public struct ObjectDirection
    {
        public ObjectOnMap obj;
        public Direction dir;

        public ObjectDirection(ObjectOnMap obj, Direction dir)
        {
            this.obj = obj;
            this.dir = dir;
        }
    }
}
