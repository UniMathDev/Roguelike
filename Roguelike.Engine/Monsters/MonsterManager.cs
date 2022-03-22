using System.Collections.Generic;
using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using Roguelike.Engine.ObjectsOnMap.FixedObjects;
using Roguelike.Engine.ObjectsOnMap;
using Roguelike.GameConfig;

namespace Roguelike.Engine.Monsters
{
    public class MonsterManager
    {
        public readonly List<Monster> monsterList;

        public readonly List<Egg> eggList;

        private Map _map;

        private Pathfinder _pathfinder;

        private Player _player;

        private int _playerTurnNumber;

        public MonsterManager(Map map, Player player)
        {
            _player = player;
            _map = map;
            monsterList = new List<Monster>();
            _pathfinder = new Pathfinder(map);
            eggList = FindEggs(map);
            _playerTurnNumber = 1;
        }

        public void OnPlayerTurnEnded(int moveNumber)
        {
            _playerTurnNumber = moveNumber;
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
                    GameLog.Add(LogMessages.DamageTakenFromMonster);
                    _player.Damage(MonsterDamage.Value);
                }
                else
                {
                    MoveTowardPlayer(monster);
                }
            }

        }

        private void GrowEggs()
        {
            if (eggList.Count == 0)
            {
                return;
            }
            List<Egg> eggsToBeRemoved = new List<Egg>();
            foreach (Egg egg in eggList)
            {
                egg.Timer--;
                if (egg.Timer == 0)
                {
                    eggsToBeRemoved.Add(egg);
                    _map.SetObjWithCoord(egg.X, egg.Y, new Floor());
                    Monster monster = new Monster(egg.X, egg.Y);
                    monster.OnDeath += OnMonsterDeath;
                    monsterList.Add(monster);
                    _map.SetObjWithCoord(egg.X, egg.Y, monster);
                }
            }
            foreach (Egg egg in eggsToBeRemoved)
            {
                eggList.Remove(egg);
            }

        }

        private List<Egg> FindEggs(Map map)
        {
            List<Egg> foundEggs = new();
            for (int i = 0; i < 4; i++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    for (int y = 0; y < map.Height; y++)
                    {
                        ObjectOnMap obj = map.GetObjWithCoord(x,y,(MapLayer)i);
                        if (obj is Egg)
                        {
                            foundEggs.Add(obj as Egg);
                        }
                    }
                }
            }
            return foundEggs;
        }

        private void MoveTowardPlayer(Monster monster)
        {
            Direction moveDirection = _pathfinder.FindWay(monster.coordinates,
                _player.coordinates, _playerTurnNumber);
            if (monster.CanMove(moveDirection, _map))
            {
                monster.Move(moveDirection, _map);
            }
        }

        private void OnMonsterDeath(LivingObject m)
        {
            Monster monster = m as Monster;
            monsterList.Remove(monster);
            _map.SetObjWithCoordToNull(monster.X, monster.Y, monster.MapLayer);
            if (_map.GetObjWithCoord(monster.X, monster.Y, new DeadMonster().MapLayer) == null)
            {
                _map.SetObjWithCoord(monster.X, monster.Y, new DeadMonster());
            }
        }
    }
}
