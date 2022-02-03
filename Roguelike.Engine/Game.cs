using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using System.Collections.Generic;

namespace Roguelike.Engine
{
    public class Game
    {
        private Map _map;

        public Player player { get; }

        public Game(Map map, Player player)
        {
            _map = map;
            this.player = player;
        }


        public string[] GetMapInStringArray(int xPos, int yPos, int width, int height)
        {
            return _map.ToStringArray(xPos, yPos, width, height);
        }

        public MapDiff InitPlayer()
        {
            return new MapDiff(player.X, player.Y, player.Character);
        }

        public void Move(Directions direction)
        {
            if (player.CanMove(direction, _map))
            {
                switch (direction)
                {
                    case Directions.Up:
                        --player.Y;
                        break;
                    case Directions.Down:
                        ++player.Y;
                        break;
                    case Directions.Right:
                        ++player.X;
                        break;
                    case Directions.Left:
                        --player.X;
                        break;
                }
            }
        }
    }
}
