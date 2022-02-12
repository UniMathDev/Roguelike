using System.Collections.Generic;
using System.Drawing;
using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using Roguelike.Engine.ObjectsOnMap.FixedObjects;

namespace Roguelike.Engine.Monsters
{
    public class MonsterManager
    {
        public readonly List<Monster> monsterList;

        public readonly List<Egg> eggList;

        private Map _map;

        private Pathfinder _pathfinder;

        private Player _player;
        public MonsterManager(Map map, Player player)
        {
            _player = player;
            _map = map;
            monsterList = new List<Monster>();
            eggList = _map.GetEggList();
            _pathfinder = new Pathfinder(map);
        }
        
        public void OnPlayerTurnEnded()
        {
            MoveMonsters();
            GrowEggs();
        }
        private void MoveMonsters()
        {
            if (monsterList.Count != 0)
            {
                foreach (Monster monster in monsterList)
                {
                    Direction moveDirection = _pathfinder.FindWay(monster.coordinates, _player.coordinates);
                    if (monster.CanMove(moveDirection, _map))
                    {
                        Point coordDiff = GameMath.DirectionToCoordDiff(moveDirection);
                        monster.X += coordDiff.X;
                        monster.Y += coordDiff.Y;
                    }
                }
            }
        }
        private void GrowEggs()
        {
            if (eggList.Count != 0)
            {
                List<Egg> eggsToBeRemoved = new List<Egg>();
                foreach (Egg egg in eggList)
                {
                    egg.Timer--;
                    if (egg.Timer == 0)
                    {
                        eggsToBeRemoved.Add(egg);
                        _map.SetObjWithCoord(egg.X, egg.Y, new Floor());
                        monsterList.Add(new Monster(egg.X, egg.Y));
                    }
                }
                foreach (Egg eggToBeRemoved in eggsToBeRemoved)
                {
                    eggList.Remove(eggToBeRemoved);
                }
            }
        }
    }
}
