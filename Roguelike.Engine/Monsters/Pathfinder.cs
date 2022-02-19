using System.Drawing;
using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;

namespace Roguelike.Engine.Monsters
{

    public class Pathfinder
    {
        Node[,] nodes;

        private readonly Map _map;
        public Pathfinder(Map map)
        {
            _map = map;
        }
        //пока что никакого пасфайндинга нет, просто идём по прямой. Если знаете как легко оформить красивее, пишите)
        public Direction FindWay(Point origin, Point destination)
        {
            Point result = new();
            if (origin.X > destination.X && _map.IsPossibleToMove(origin.X - 1, origin.Y)) 
            {
                result.X = -1;
            }
            else if(origin.X < destination.X && _map.IsPossibleToMove(origin.X + 1, origin.Y))
            {
                result.X = 1;
            }
            else
            {
                result.X = 0;
            }

            if (origin.Y > destination.Y && _map.IsPossibleToMove(origin.X, origin.Y - 1))
            {
                result.Y = -1;
            }
            else if (origin.Y < destination.Y && _map.IsPossibleToMove(origin.X, origin.Y + 1))
            {
                result.Y = 1;
            }
            else
            {
                result.Y = 0;
            }
            return GameMath.CoordDiffToDirection(result);
        }


        class Node
        {
            int Gcost;
            int Hcost;
            int Fcost;

            Node connection;
        }
    }
}
