using System;
using System.Drawing;
using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using System.Collections.Generic;
using Roguelike.GameConfig;

namespace Roguelike.Engine.Monsters
{

    public class Pathfinder
    {
        Node[,] nodes;

        private readonly Map _map;
        public Pathfinder(Map map)
        {
            _map = map;
            nodes = new Node[_map.Height, _map.Width];
        }

        public Direction FindWay(Point origin, Point destination, int playerTurnNumber)
        {
            for (int y = 0; y < _map.Height; y++)
            {
                for (int x = 0; x < _map.Width; x++)
                {
                    nodes[y, x] = (_map.GetTopObjWithCoord(x, y).Walkable ?
                        new Node(new Point(x, y), int.MaxValue,
                        Math.Abs(destination.X - x) + Math.Abs(destination.Y - y), false) : null);
                }
            }

            nodes[destination.Y, destination.X] = new Node(destination,
                int.MaxValue, 0, false);

            int minPathLength = int.MaxValue;
            Point result = new(0, 0);
            var possibleMoves = new List<Point>();

            int pathLength = 0;
            for (int y = origin.Y - 1; y <= origin.Y + 1; y++)
            {
                for(int x = origin.X - 1; x <= origin.X + 1; x++)
                {
                    if ((x == origin.X && y == origin.Y) || nodes[y, x] == null)
                        continue;
                    
                    possibleMoves.Add(new Point(x - origin.X, y - origin.Y));

                    pathLength = GetPathLength(nodes[y, x], nodes[destination.Y, destination.X]);
                    if (pathLength < minPathLength)
                    {
                        minPathLength = pathLength;
                        result.X = x - origin.X;
                        result.Y = y - origin.Y;
                    }
                }
            }

            if (minPathLength == int.MaxValue || 
                ((Math.Pow(origin.X - destination.X, 2) +
                Math.Pow(origin.Y - destination.Y, 2) > Math.Pow(MonsterFOV.Value, 2)) &&
                playerTurnNumber < MonsterFOV.СriticalPlayerTurn))
            {
                result = possibleMoves[GameMath.rand.Next(0, possibleMoves.Count)];
            }

            return GameMath.CoordDiffToDirection(result);
        }

        private int GetPathLength(Node origin, Node destination)
        {
            for (int y = 0; y < _map.Height; y++)
            {
                for (int x = 0; x < _map.Width; x++)
                {
                    if (nodes[y, x] == null)
                        continue;
                    nodes[y, x].isTraversed = false;
                    nodes[y, x].distanceFromStart = int.MaxValue;
                }
            }

            Node currentNode = origin;
            Node newCurrentNode = null;
            origin.distanceFromStart = 0;
            while(currentNode != destination && currentNode != null)
            {
                currentNode.isTraversed = true;

                for (int y = currentNode.coords.Y - 1; y <= currentNode.coords.Y + 1; y++)
                {
                    for (int x = currentNode.coords.X - 1; x <= currentNode.coords.X + 1; x++)
                    {
                        if (nodes[y, x] == null || nodes[y, x].isTraversed == true)
                            continue;

                        nodes[y, x].distanceFromStart = 
                            Math.Min(nodes[y, x].distanceFromStart, currentNode.distanceFromStart + 1);

                        if (newCurrentNode == null)
                        {
                            newCurrentNode = nodes[y, x];
                            continue;
                        }
                        newCurrentNode =
                            ((nodes[y, x].distanceFromStart + nodes[y, x].distanceToDestination) <
                            (newCurrentNode.distanceFromStart + newCurrentNode.distanceToDestination) ?
                            nodes[y, x] : newCurrentNode);
                    }
                }

                currentNode = newCurrentNode;
                newCurrentNode = null;
            }

            return destination.distanceFromStart;
        }

        class Node
        {
            public Point coords;
            public int distanceFromStart;
            public int distanceToDestination;
            public bool isTraversed;

            public Node(Point coords, int distanceFromStart, int distanceToDestination, bool isTraversed)
            {
                this.coords = coords;
                this.distanceFromStart = distanceFromStart;
                this.distanceToDestination = distanceToDestination;
                this.isTraversed = isTraversed;
            }
        }
    }
}
