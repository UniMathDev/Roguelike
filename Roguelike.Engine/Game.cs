using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using Roguelike.Engine.Monsters;
using System.Drawing;

namespace Roguelike.Engine
{
    public class Game
    {
        public Map _map { get; }

        public Player player { get; }

        public MonsterManager _monsterManager { get; }

        public Game(Map map, Player player)
        {
            _map = map;
            this.player = player;
            _monsterManager = new(map,player);
        }

        public string[] GetMapInStringArray(int xPos, int yPos, int width, int height)
        {
            return _map.ToStringArray(xPos, yPos, width, height);
        }

        public void Move(Direction direction)
        {
            if (player.CanMove(direction, _map))
            {
                Point coordDiff = GameMath.DirectionToCoordDiff(direction);
                player.X += coordDiff.X;
                player.Y += coordDiff.Y;
                _monsterManager.OnPlayerTurnEnded();
            }
        }
        public void Use(int X, int Y, object useWith)
        {
            _map.GetObjWithCoord(X, Y).Use(useWith);
            _monsterManager.OnPlayerTurnEnded();
        }
        public void Wait()
        {
            _monsterManager.OnPlayerTurnEnded();
        }
    }
}
