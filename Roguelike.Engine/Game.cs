using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using Roguelike.Engine.Monsters;
using Roguelike.Engine.ObjectsOnMap;
using Roguelike.Engine.ObjectsOnMap.FixedObjects;
using Roguelike.Engine.InventoryObjects;
using System.Drawing;
using System;
using System.Collections.Generic;

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
            player.OnDeath += OnPlayerDeath;
            _monsterManager = new(map,player);

            //TEST
            InventoryObjectOnGround obj = new InventoryObjectOnGround();
            map.SetObjWithCoord(18, 2, obj);
            obj.Inventory.Add(new Axe());

            InventoryObjectOnGround obj2 = new InventoryObjectOnGround();
            map.SetObjWithCoord(15, 4, obj2);
            obj2.Inventory.Add(new Pen());

            InventoryObjectOnGround obj3 = new InventoryObjectOnGround();
            map.SetObjWithCoord(15, 7, obj3);
            obj3.Inventory.Add(new Axe());

            InventoryObjectOnGround obj4 = new InventoryObjectOnGround();
            map.SetObjWithCoord(16, 8, obj4);
            obj4.Inventory.Add(new Pen());
            obj4.Inventory.Add(new Pen());
            obj4.Inventory.Add(new Pen());
            obj4.Inventory.Add(new Pen());
            obj4.Inventory.Add(new Pen());
            obj4.Inventory.Add(new Pen());
            //
        }

        public void Move(Direction direction)
        {
            if (player.CanMove(direction, _map))
            {
                player.Move(direction, _map);
                _monsterManager.OnPlayerTurnEnded();
            }
        }
        public void Interact(int X, int Y, object useWith)
        {
            ObjectOnMap obj = _map.GetTopObjWithCoord(X, Y);
            if (player.NextTo(X,Y)) 
            {
                if (obj is IUsable)
                {
                    (obj as IUsable).Use(useWith);

                    _monsterManager.OnPlayerTurnEnded();
                    return;
                }
                if (obj is ISearchable)
                {
                    //
                    //в идеале:
                    //GUI.displayObjectInventory((obj as ISearchable).Inventory,X,Y);
                    //по дальнейшему нажатию можно подобрать чтото определенное, но пока так:
                    //
                    List<InventoryObject> addedItems = new();
                    foreach (InventoryObject item in (obj as ISearchable).Inventory)
                    {
                        if (player.inventory.CanAddToInventory(item))
                        {
                            player.inventory.AddToInventory(item);
                            addedItems.Add(item);
                        }
                    }
                    foreach (var item in addedItems)
                    {
                        (obj as ISearchable).Inventory.Remove(item);
                    }

                    _monsterManager.OnPlayerTurnEnded();
                    return;
                }
                if (obj is LivingObject && !(obj is Player))
                {
                    Monster monster = (obj as Monster);
                    Weapon weapon = player.inventory.ActiveWeapon;
                    if (weapon is MeleeWeapon || weapon == null)
                        HitMonster(monster, weapon as MeleeWeapon);
                    else
                        ShootMonster(monster, weapon as RangedWeapon);

                    _monsterManager.OnPlayerTurnEnded();
                    return;
                }
            }
        }
        public void Wait()
        {
            _monsterManager.OnPlayerTurnEnded();
        }

        private void OnPlayerDeath(LivingObject player)
        {
            Environment.Exit(0);
        }
        private void HitMonster(Monster monster , MeleeWeapon weapon)
        {
            float damageAmount = 30; //Заменить на FistDamage из конфига. (сейчас он из этого места в коде недоступен.)

            if (weapon != null)
            {
                damageAmount = weapon.AverageDamage - weapon.DamageRange / 2
                    + weapon.DamageRange * (float)GameMath.rand.NextDouble();
                bool knockedBack = weapon.KnockBackChance - (float)GameMath.rand.NextDouble() <= 0;
                if (knockedBack)
                {
                    Point coordDiff = new(monster.X - player.X, monster.Y - player.Y);
                    monster.Move(GameMath.CoordDiffToDirection(coordDiff), _map);
                }
                weapon.Durability -= 1;
                if(weapon.Durability <= 0)
                {
                    BreakWeapon(weapon);
                }
            }
            monster.Damage(damageAmount);
        }
        private void BreakWeapon(MeleeWeapon weapon)
        {
            player.inventory.RemoveFromInventory(weapon);
            weapon = null;
        }
        private void ShootMonster(Monster monster, RangedWeapon weapon)
        {
            throw new NotImplementedException();
        }
    }
}
