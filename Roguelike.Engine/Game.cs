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

            //TESTING GROUND ITEMS
            Wardrobe obj = new Wardrobe();
            map.SetObjWithCoord(18, 2, obj);
            (obj as ISearchable).Inventory.Add(new Axe());

            InventoryObjectOnGround obj2 = new InventoryObjectOnGround();
            map.SetObjWithCoord(15, 4, obj2);
            obj2.Inventory.Add(new Pen());

            InventoryObjectOnGround obj3 = new InventoryObjectOnGround();
            map.SetObjWithCoord(15, 7, obj3);
            obj3.Inventory.Add(new Axe());

            InventoryObjectOnGround obj4 = new InventoryObjectOnGround();
            map.SetObjWithCoord(16, 8, obj4);
            obj4.Inventory.Add(new LightBulb());

            //TESTING LAMPS ITEMS
            Lamp lamp = new();
            _map.SetObjWithCoord(13,5,lamp);


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
        public void Interact(int X, int Y)
        {
            if (!_map.WithinBounds(X, Y))
            {
                return;
            }
            ObjectOnMap obj = _map.GetTopObjWithCoord(X, Y);
            object useWith = player.inventory.ActiveTool;
            if (player.NextTo(X,Y)) 
            {
                if (obj is IUsable)
                {
                    (obj as IUsable).TryUse(useWith);

                    _monsterManager.OnPlayerTurnEnded();
                    player.inventory.SetActiveInventoryToolToNull();
                    return;
                }
                player.inventory.SetActiveInventoryToolToNull();
                if (obj is ISearchable)
                {
                    //
                    //в идеале:
                    //GUI.displayObjectInventory((obj as ISearchable).Inventory,X,Y);
                    //по дальнейшему нажатию можно подобрать чтото определенное, но пока так:
                    //
                    List<InventoryObject> addedItems = new();
                    List<InventoryObject> itemsOnGround = (obj as ISearchable).Inventory;
                    foreach (InventoryObject item in itemsOnGround)
                    {
                        if (player.inventory.TryAddToInventory(item))
                        {
                            addedItems.Add(item);
                        }
                    }

                    if (addedItems.Count == itemsOnGround.Count)
                    {
                        _map.SetObjWithCoordToNull(X,Y, new InventoryObjectOnGround().MapLayer);
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
        public void TrySwitchActiveInventoryItem(int handIndex)
        {
            InventoryObject iObj = player.inventory.Hands[handIndex];
            if (!player.inventory.TrySetActiveInventoryItem(iObj, true))
            {
                player.inventory.TrySetActiveInventoryItem(iObj, false);
            }
        }
        public void DropItem(int index, bool fromHands)
        {
            InventoryObject iObj;
            if (fromHands)
            {
                iObj = player.inventory.Hands[index];
            }
            else
            {
                iObj = player.inventory.Pockets[index];
            }
            ObjectOnMap objUnderPlayer =
                _map.GetObjWithCoord(player.X, player.Y, MapLayer.SECONDARY);
            if (objUnderPlayer == null)
            {
                InventoryObjectOnGround newInvObjOnGround = new();
                (newInvObjOnGround as ISearchable).Inventory.Add(iObj);
                _map.SetObjWithCoord(player.X, player.Y, newInvObjOnGround);
            }
            else if (objUnderPlayer is InventoryObjectOnGround)
            {
                (objUnderPlayer as ISearchable).Inventory.Add(iObj);
            }
            player.inventory.RemoveFromInventory(iObj);
            _monsterManager.OnPlayerTurnEnded();
        }
        public void UnpocketItem(int index)
        {
            InventoryObject iObj = player.inventory.Pockets[index];
            if (player.inventory.TryAddToHands(iObj))
            {
                player.inventory.Pockets.RemoveAt(index);
                _monsterManager.OnPlayerTurnEnded();
            }
        }
        public void PocketItem(int index)
        {
            InventoryObject iObj = player.inventory.Hands[index];
            if (player.inventory.TryAddToPockets(iObj))
            {
                player.inventory.Hands[index] = null;
                _monsterManager.OnPlayerTurnEnded();
            }
        }
        private void OnPlayerDeath(LivingObject player)
        {
            Environment.Exit(0);
        }
        private void HitMonster(Monster monster , MeleeWeapon weapon)
        {
            float damageAmount = 30f; //Заменить на FistDamage из конфига. (сейчас он из этого места в коде недоступен.)

            if (weapon != null)
            {
                damageAmount = weapon.AverageDamage - weapon.DamageRange / 2f
                    + weapon.DamageRange * (float)GameMath.rand.NextDouble();
                bool knockedBack = weapon.KnockBackChance - (float)GameMath.rand.NextDouble() >= 0f;
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
