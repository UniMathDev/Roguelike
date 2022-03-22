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

        public Action PlayerTookDamage;
        public Action PlayerShotGun;
        public Game(Map map, Player player)
        {
            this.map = map;
            this.player = player;
            player.OnDeath += OnPlayerDeath;
            monsterManager = new(map, player);
            playerTurnNumber = 1;

            //TESTING GROUND ITEMS
            Wardrobe obj = new Wardrobe();
            map.SetObjWithCoord(18, 2, obj);
            (obj as ISearchable).Inventory.Add(new Bandage());

            InventoryObjectOnGround obj2 = new InventoryObjectOnGround();
            map.SetObjWithCoord(15, 4, obj2);
            obj2.Inventory.Add(new Gun());
            obj2.Inventory.Add(new Magazine(7));
            obj2.Inventory.Add(new Magazine(4));
            obj2.Inventory.Add(new Magazine(10));

            InventoryObjectOnGround obj3 = new InventoryObjectOnGround();
            map.SetObjWithCoord(15, 7, obj3);
            obj3.Inventory.Add(new Axe());

            InventoryObjectOnGround obj4 = new InventoryObjectOnGround();
            map.SetObjWithCoord(16, 8, obj4);
            obj4.Inventory.Add(new LightBulb());

            //TESTING LAMPS ITEMS
            Lamp lamp1 = new();
            this.map.SetObjWithCoord(13, 5, lamp1);
            Lamp lamp2 = new();
            this.map.SetObjWithCoord(28, 8, lamp2);
            Lamp lamp3 = new();
            this.map.SetObjWithCoord(40, 8, lamp3);
            Lamp lamp4 = new();
            this.map.SetObjWithCoord(52, 8, lamp4);
            Lamp lamp5 = new();
            this.map.SetObjWithCoord(64, 8, lamp5);
            Lamp lamp6 = new();
            this.map.SetObjWithCoord(76, 12, lamp6);

            //

            UpdateFOV();
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

        public void UpdateFOV()
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    map.GetTopObjWithCoord(x, y).CurrentForegroundColor = null;
                    map.GetTopObjWithCoord(x, y).InFOV = false;
                }
            }

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    ObjectOnMap obj = map.GetTopObjWithCoord(x, y);
                    ObjectOnMap ceilObj = map.GetObjWithCoord(x, y, MapLayer.CEILING);
                    if (obj is ILuminous)
                    {
                        SetColorForIlluminatedArea(new Point(x, y), (obj as ILuminous).LightedAreaRadius);
                    }

                    if (ceilObj is ILuminous)
                    {
                        SetColorForIlluminatedArea(new Point(x, y), (ceilObj as ILuminous).LightedAreaRadius);
                    }
                }
            }

            SetColorForIlluminatedArea(new Point(player.X, player.Y), player.FOVSize);
        }

        private void SetColorForIlluminatedArea(Point lightSourceCoords, int lightedAreaRadius)
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (inIlluminatedArea(x, y, lightedAreaRadius, lightSourceCoords))
                    {
                        if (!inIlluminatedArea(x, y, PlayerStats.LightSourceVisibilityDistance, new Point(player.X, player.Y)))
                        {
                            continue;
                        }

                        ObjectOnMap objectOnMap = map.GetTopObjWithCoord(x, y);                        

                        double distanceFromLightSourceSquared =
                            Math.Pow(x - lightSourceCoords.X, 2) / 4 +
                            Math.Pow(y - lightSourceCoords.Y, 2);
                        double lightedAreaRadiusSquared = Math.Pow(lightedAreaRadius, 2);
                        objectOnMap.InFOV = true;

                        if (distanceFromLightSourceSquared <
                            lightedAreaRadiusSquared * 0.4)
                        {
                            objectOnMap.CurrentForegroundColor = 
                                ChooseLightedObjColor(objectOnMap.CurrentForegroundColor, ConsoleColor.White);
                        }
                        else if (distanceFromLightSourceSquared <
                            lightedAreaRadiusSquared * 0.7)
                        {
                            objectOnMap.CurrentForegroundColor =
                                ChooseLightedObjColor(objectOnMap.CurrentForegroundColor, ConsoleColor.Gray);
                        }
                        else if (distanceFromLightSourceSquared <
                            lightedAreaRadiusSquared)
                        {
                            objectOnMap.CurrentForegroundColor =
                                ChooseLightedObjColor(objectOnMap.CurrentForegroundColor, ConsoleColor.DarkGray);
                        }
                        else
                        {
                            objectOnMap.InFOV = false;
                        }
                    }
                }
            }
        }

        private ConsoleColor? ChooseLightedObjColor(ConsoleColor? currentColor, ConsoleColor newColor)
        {
            if (currentColor == ConsoleColor.White || newColor == ConsoleColor.White)
            {
                return ConsoleColor.White;
            }

            if (currentColor == ConsoleColor.Gray || newColor == ConsoleColor.Gray)
            {
                return ConsoleColor.Gray;
            }

            if (currentColor == ConsoleColor.DarkGray || newColor == ConsoleColor.DarkGray)
            {
                return ConsoleColor.DarkGray;
            }

            return currentColor;
        }

        public void Interact(int X, int Y)
        {
            if (!map.InPlayerFOV(X, Y))
            {
                return;
            }

            ObjectOnMap obj = map.GetTopObjWithCoord(X, Y);
            object useWith = player.inventory.ActiveTool;
            bool playerNextToObject = player.NextTo(X, Y);

            if (obj is IUsable && player.NextTo(X, Y))
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
            if (obj is ISearchable && player.NextTo(X, Y))
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
                if (weapon is RangedWeapon && monster.InFOV)
                {
                    ShootMonster(monster, weapon as RangedWeapon);
                    OnPlayerTurnEnded();
                }
                else if (weapon is MeleeWeapon || weapon == null && player.NextTo(X, Y))
                {
                    HitMonster(monster, weapon as MeleeWeapon);
                    OnPlayerTurnEnded();
                }
                return;
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
        public void ReloadGun()
        {
            PlayerInventory inventory = player.inventory;
            Magazine bestMagazine = null;
            int bestAmmoCount = 0;
            Gun playerGun = null;
            bool playerHasGunInHands = false;
            for (int handIndex = 0; handIndex < 2; handIndex++)
            {
                if (inventory.Hands[handIndex] is Gun)
                {
                    playerGun = inventory.Hands[handIndex] as Gun;
                    playerHasGunInHands = true;
                }
            }
            if (!playerHasGunInHands)
            {
                return;
            }

            for (int handIndex = 0; handIndex < 2; handIndex++)
            {
                AssignIfBestMag(inventory.Hands[handIndex]);
            }
            for (int i = 0; i < inventory.Pockets.Count; i++)
            {
                AssignIfBestMag(inventory.Pockets[i]);
            }
            if (bestMagazine != null)
            {
                int buffer;
                buffer = playerGun.Ammo;
                playerGun.Ammo = bestMagazine.Ammo;
                bestMagazine.Ammo = buffer;
                if(bestMagazine.Ammo == 0)
                {
                    inventory.RemoveFromInventory(bestMagazine);
                }
                OnPlayerTurnEnded();
            }
            void AssignIfBestMag(InventoryObject item)
            {
                if (item is Magazine &&
                   (item as Magazine).Ammo > bestAmmoCount)
                {
                    bestMagazine = (item as Magazine);
                    bestAmmoCount = (item as Magazine).Ammo;
                }
            }
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


        private bool TryMove(Direction direction)
        {
            if (player.CanMove(direction, map))
            {
                player.Move(direction, map);
                return true;
            }
            return false;
        }

        private bool inIlluminatedArea(int x, int y, int fovSize, Point lightSourceCoords)
        {
            if ((Math.Pow(x - lightSourceCoords.X, 2) / 4 +
                            Math.Pow(y - lightSourceCoords.Y, 2)) < Math.Pow(fovSize, 2))
            {
                bool inIlluminatedArea = true;

                if (lightSourceCoords.X == x && lightSourceCoords.Y == y)
                    return true;

                int coordY;

                if (lightSourceCoords.X == x)
                {
                    int coordX = x;
                    int offset = (lightSourceCoords.Y < y ? 1 : -1);
                    for (coordY = lightSourceCoords.Y + offset; coordY != y; coordY += offset)
                    {
                        if (!map.GetTopObjWithCoord(coordX, coordY).Seethrough)
                        {
                            inIlluminatedArea = false;
                            break;
                        }
                    }

                    return inIlluminatedArea;
                }


                //y = kx + b
                double k = ((double)lightSourceCoords.Y - y) / ((double)lightSourceCoords.X - x);
                double b1 = 0;
                double b2 = 0;

                if (k < 0)
                {
                    b1 = (double)y + 0.1 - k * ((double)x + 0.1);
                    b2 = (double)y + 0.9 - k * ((double)x + 0.9);
                }
                else
                {
                    b1 = (double)y + 0.9 - k * ((double)x + 0.1);
                    b2 = (double)y + 0.1 - k * ((double)x + 0.9);
                }

                double offsetX = (lightSourceCoords.X < x ? 0.05 : -0.05);

                for (double coordX = lightSourceCoords.X + 0.1;
                    (int)Math.Floor(coordX) != x || (int)Math.Floor(k * coordX + b1) != y; coordX += offsetX)
                {
                    coordY = (int)Math.Floor(k * coordX + b1);
                    if (!map.GetTopObjWithCoord((int)Math.Floor(coordX), coordY)
                        .Seethrough)
                    {
                        inIlluminatedArea = false;
                        break;
                    }
                }

                if (inIlluminatedArea == true)
                    return inIlluminatedArea;

                inIlluminatedArea = true;

                for (double coordX = lightSourceCoords.X + 0.9;
                    (int)Math.Floor(coordX) != x || (int)Math.Floor(k * coordX + b2) != y; coordX += offsetX)
                {
                    coordY = (int)Math.Floor(k * coordX + b2);
                    if (!map.GetTopObjWithCoord((int)Math.Floor(coordX), coordY)
                        .Seethrough)
                    {
                        inIlluminatedArea = false;
                        break;
                    }
                }

                return inIlluminatedArea;
            }

            return false;
        }

        private void OnPlayerDeath(LivingObject player)
        {
            Console.Clear();
            Environment.Exit(0);
        }

        private void HitMonster(Monster monster, MeleeWeapon weapon)
        {
            float damageAmount = PlayerStats.FistDamage;

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
            if (weapon.Ammo > 0) {
                bool Hit = weapon.HitChance - GameMath.rand.NextDouble() > 0;
                if (Hit)
                {
                    float damageAmount = weapon.AverageDamage - weapon.DamageRange / 2f
                           + weapon.DamageRange * (float)GameMath.rand.NextDouble();
                    monster.Damage(damageAmount);
                    GameLog.Add(LogMessages.PistolHit);
                }
                else
                {
                    GameLog.Add(LogMessages.PistolMiss);
                }
                weapon.Ammo--;
                PlayerShotGun.Invoke();
                
                OnPlayerTurnEnded();
            }
            else
            {
                GameLog.Add(LogMessages.PistolEmpty);
                OnPlayerTurnEnded();
            }
        }

        private void OnPlayerTurnEnded()
        {
            float oldPlayerHealth = player.Health;
            playerTurnNumber++;

            player.Stamina = MathF.Min(player.Stamina + PlayerStats.EndOfTurnStaminaGain,
                                       PlayerStats.MaxStamina);
            monsterManager.OnPlayerTurnEnded(playerTurnNumber);
            if (oldPlayerHealth > player.Health)
            {
                PlayerTookDamage.Invoke();
            }

            UpdateFOV();
        }
    }
}
