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
            ActWithMonsters();
            GrowEggs();
        }
        private void ActWithMonsters()
        {
            if (monsterList.Count == 0)
            {
                return;
            }

            foreach (Monster monster in monsterList)
            {
                if (monster.NextTo(_player.X, _player.Y))
                {
                    _player.Damage(10f);
                }
                else
                {
                    MoveTowardPlayer(monster);
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
        private void MoveTowardPlayer(Monster monster)
        {
            Direction moveDirection = _pathfinder.FindWay(monster.coordinates, _player.coordinates);
            if (monster.CanMove(moveDirection, _map, monsterList, _player))
            {
                Point coordDiff = GameMath.DirectionToCoordDiff(moveDirection);
                monster.MoveBy(coordDiff.X, coordDiff.Y);
            }
        }
    }
}
