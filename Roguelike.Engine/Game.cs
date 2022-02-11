using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using Roguelike.Engine.Monsters;
using System.Drawing;
using System;

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
            if (player.CanMove(direction, _map, _monsterManager.monsterList))
            {
                Point coordDiff = GameMath.DirectionToCoordDiff(direction);
                player.MoveBy(coordDiff.X, coordDiff.Y);
                _monsterManager.OnPlayerTurnEnded();
            }
        }
        public void Use(int X, int Y, object useWith)
        {
            if (player.NextTo(X,Y)) 
            {
                _map.GetObjWithCoord(X, Y).Use(useWith);
                _monsterManager.OnPlayerTurnEnded();
            }
        }
        public void Wait()
        {
            _monsterManager.OnPlayerTurnEnded();
        }

    }
}
