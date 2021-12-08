using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;

namespace Roguelike.Engine
{
    public class Game
    {
        private Map _map;
        private Player _player;


        public Game(Map map, Player player)
        {
            _map = map;
            _player = player;
        }


        public string MapInString
        {
            get { return _map.ToString(); }
        }

        public MapDiff InitPlayer()
        {
            return new MapDiff(_player.X, _player.Y, _player.Character);
        }

        public MapDiff[] Move(Directions direction)
        {
            if (_player.CanMove(direction, _map))
            {
                int x = _player.X;
                int y = _player.Y;
                int newPlayerX = x;
                int newPlayerY = y;
                switch (direction)
                {
                    case Directions.Up:
                        newPlayerY = --_player.Y;
                        break;
                    case Directions.Down:
                        newPlayerY = ++_player.Y;
                        break;
                    case Directions.Right:
                        newPlayerX = ++_player.X;
                        break;
                    case Directions.Left:
                        newPlayerX = --_player.X;
                        break;
                }
                return new MapDiff[]
                {
                    new MapDiff(x, y, _map.GetCharWithCoord(x, y)),
                    new MapDiff(newPlayerX, newPlayerY, _player.Character)
                };

            }
            return new MapDiff[0];
        }


    }
}
