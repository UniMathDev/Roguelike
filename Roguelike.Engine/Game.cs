using Roguelike.Engine.Enums;
using Roguelike.Engine.InventoryObjects;
using Roguelike.Engine.Maps;
using Roguelike.Engine.Monsters;
using Roguelike.Engine.ObjectsOnMap;
using Roguelike.Engine.ObjectsOnMap.FixedObjects;
using Roguelike.GameConfig;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Roguelike.Engine
{
    public class Game
    {
        public Map map { get; }

        public Player player { get; }

        public MonsterManager monsterManager { get; }

        public int playerTurnNumber { get; private set; }

        public Game(Map map, Player player)
        {
            this.map = map;
            this.player = player;
            player.OnDeath += OnPlayerDeath;
            monsterManager = new(map, player);
            playerTurnNumber = 1;

            UpdateFOV();

            //TESTING GROUND ITEMS
            Wardrobe obj = new Wardrobe();
            map.SetObjWithCoord(18, 2, obj);
            (obj as ISearchable).Inventory.Add(new Bandage());

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
            this.map.SetObjWithCoord(13, 5, lamp);


            //
        }
        public void Walk(Direction direction)
        {
            if (TryMove(direction))
            {
                player.Stamina -= PlayerStats.WalkStaminaPenalty;
                OnPlayerTurnEnded();
            }
        }

        public void Run(Direction direction)
        {
            if (player.Stamina - PlayerStats.RunStaminaPenalty < 0)
            {
                Walk(direction);
                return;
            }

            bool success = false;
            if (TryMove(direction))
            {
                success = true;
                TryMove(direction);
            }

            if (success)
            {
                player.Stamina -= PlayerStats.RunStaminaPenalty;
                OnPlayerTurnEnded();
            }
        }
        private bool TryMove(Direction direction)
        {
            if (player.CanMove(direction, map))
            {
                player.Move(direction, map);
                return true;
            }

            player.Move(direction, map);
            monsterManager.OnPlayerTurnEnded(playerTurnNumber);
            return false;
        }

        public void UpdateFOV()
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    map.GetTopObjWithCoord(x, y).InFOV = false;
                    if ((Math.Pow(x - player.X, 2) / 4 +
                            Math.Pow(y - player.Y, 2)) < Math.Pow(player.FOVSize, 2))
                    {
                        map.GetTopObjWithCoord(x, y).InFOV = true;

                        if (player.X == x && player.Y == y)
                            continue;

                        if (player.X == x)
                        {
                            int i = x;
                            int offset = (player.Y < y ? 1 : -1);
                            for (int j = player.Y + offset; j != y; j += offset)
                            {
                                if (!map.GetTopObjWithCoord(i, j).Seethrough)
                                {
                                    map.GetTopObjWithCoord(x, y).InFOV = false;
                                    break;
                                }
                            }

                            continue;
                        }

                        //y = kx + b
                        double k = ((double)player.Y - y) / ((double)player.X - x);
                        double b = (double)y + 0.5 - k * ((double)x + 0.5);

                        int j1;
                        double offsetX = (player.X < x ? 0.05 : -0.05);
                        for (double i = player.X + 0.5;
                            (int)Math.Floor(i) != x || (int)Math.Floor(k * i + b) != y; i += offsetX)
                        {
                            j1 = (int)Math.Floor(k * i + b);
                            j1 = (j1 < 0 ? 0 : j1);
                            j1 = (j1 >= map.Height ? map.Height - 1 : j1);
                            if (!map.GetTopObjWithCoord((int)Math.Floor(i), j1).Seethrough)
                            {
                                map.GetTopObjWithCoord(x, y).InFOV = false;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void Interact(int X, int Y)
        {
            if (!map.WithinBounds(X, Y))
            {
                return;
            }

            ObjectOnMap obj = map.GetTopObjWithCoord(X, Y);
            object useWith = player.inventory.ActiveTool;

            if (player.NextTo(X, Y)) 
            {
                if (obj is IUsable)
                {
                    UseCallBack callback = (obj as IUsable).TryUse(useWith);
                    if (callback.ItemUsedUp) 
                    {
                        player.inventory.RemoveFromInventory(useWith as InventoryObject); 
                    }
                    if (!callback.Success)
                    {
                        GameLog.Add(LogMessages.UseUnsuccessful);
                    }
                    OnPlayerTurnEnded();
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
                        map.SetObjWithCoordToNull(X, Y, new InventoryObjectOnGround().MapLayer);
                    }

                    foreach (var item in addedItems)
                    {
                        (obj as ISearchable).Inventory.Remove(item);
                    }

                    OnPlayerTurnEnded();
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

                    OnPlayerTurnEnded();
                    return;
                }
            }
        }
        public void Wait()
        {
            OnPlayerTurnEnded();
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
                map.GetObjWithCoord(player.X, player.Y, MapLayer.SECONDARY);
            if (objUnderPlayer == null)
            {
                InventoryObjectOnGround newInvObjOnGround = new();
                (newInvObjOnGround as ISearchable).Inventory.Add(iObj);
                map.SetObjWithCoord(player.X, player.Y, newInvObjOnGround);
            }
            else if (objUnderPlayer is InventoryObjectOnGround)
            {
                (objUnderPlayer as ISearchable).Inventory.Add(iObj);
            }
            player.inventory.RemoveFromInventory(iObj);

            OnPlayerTurnEnded();
        }

        public void UnpocketItem(int index)
        {
            InventoryObject iObj = player.inventory.Pockets[index];
            if (player.inventory.TryAddToHands(iObj))
            {
                player.inventory.Pockets.RemoveAt(index);

                OnPlayerTurnEnded();
            }
        }
        public void PocketItem(int index)
        {
            InventoryObject iObj = player.inventory.Hands[index];
            if (player.inventory.TryAddToPockets(iObj))
            {
                player.inventory.Hands[index] = null;

                OnPlayerTurnEnded();

            }
        }
        private void OnPlayerDeath(LivingObject player)
        {
            Console.Clear();
            Environment.Exit(0);
        }
        private void HitMonster(Monster monster, MeleeWeapon weapon)
        {
            float damageAmount = GameConfig.PlayerStats.FistDamage;

            if (weapon != null)
            {
                damageAmount = weapon.AverageDamage - weapon.DamageRange / 2f
                    + weapon.DamageRange * (float)GameMath.rand.NextDouble();
                bool knockedBack = weapon.KnockBackChance - (float)GameMath.rand.NextDouble() >= 0f;
                if (knockedBack)
                {
                    Point coordDiff = new(monster.X - player.X, monster.Y - player.Y);
                    monster.Move(GameMath.CoordDiffToDirection(coordDiff), map);
                }
                weapon.Durability -= 1;
                if (weapon.Durability <= 0)
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
        private void OnPlayerTurnEnded()
        {
            playerTurnNumber++;

            UpdateFOV();
            player.Stamina = MathF.Min(player.Stamina + PlayerStats.EndOfTurnStaminaGain,
                                       PlayerStats.MaxStamina);
            monsterManager.OnPlayerTurnEnded(playerTurnNumber);
        }
    }
}
