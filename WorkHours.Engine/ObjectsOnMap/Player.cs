using System.Collections.Generic;
using System;
using Roguelike.Engine.InventoryObjects;

namespace Roguelike.Engine.ObjectsOnMap
{
    public class Player : LivingObject
    {
        public PlayerInventory inventory = new PlayerInventory();
        public float Stamina { get; set; } = GameConfig.PlayerStats.MaxStamina;
        public Player(int x, int y)
        {
            Character = '@';
            ForegroundColor = ConsoleColor.Green;
            Description = "Me: this is me. ";
            Health = GameConfig.PlayerStats.MaxHealth;
            X = x;
            Y = y;
        }
    }
    public class PlayerInventory
    {
        public Action InventoryUpdated;
        public List<InventoryObject> Pockets { get; private set; } = new List<InventoryObject>();
        private int RemainingPocketSpace = GameConfig.PlayerStats.PocketSize;
        public InventoryObject[] Hands { get; private set; } = new InventoryObject[2];
        public Weapon ActiveWeapon { get; private set; }
        public InventoryObject ActiveTool { get; private set; }
        public bool TryAddToInventory(InventoryObject iObj)
        {
            if (TryAddToHand(iObj,true))
            {
                return true;
            }
            if (TryAddToHand(iObj,false))
            {
                return true;
            }
            if (TryAddToPockets(iObj))
            {
                return true;
            }
            return false;
        }
        public bool TryAddToHands(InventoryObject iObj)
        {
            if (TryAddToHand(iObj, true))
            {
                return true;
            }
            else if (TryAddToHand(iObj, false))
            {
                return true;
            }
            return false;
        }
        public bool TryAddToHand(InventoryObject iObj, bool toRightHand)
        {
            int hand;
            if (toRightHand) 
            {
                hand = 0;
            }
            else
            {
                hand = 1;
            }
            
            if (Hands[hand] == null && (Hands[1 - hand] == null || (Hands[1 - hand].TwoHanded == false && !iObj.TwoHanded)))
            {
                if (iObj is Weapon) 
                {
                    TrySetActiveInventoryItem(iObj, true);
                }
                Hands[hand] = iObj;
                InventoryUpdated.Invoke();
                return true;
            }
            return false;
        }
        public bool TryAddToPockets(InventoryObject iObj)
        {
            if (RemainingPocketSpace - iObj.Size >= 0)
            {
                Pockets.Add(iObj);
                RemainingPocketSpace -= iObj.Size;
                InventoryUpdated.Invoke();
                return true;
            }
            return false;
        }
        public void RemoveFromInventory(InventoryObject iObj)
        {
            if (Hands[0] == iObj)
            {
                Hands[0] = null;
                TrySetActiveInventoryItem(iObj, false);
                InventoryUpdated.Invoke();
                return;
            }
            if (Hands[1] == iObj)
            {
                Hands[1] = null;
                TrySetActiveInventoryItem(iObj, false);
                InventoryUpdated.Invoke();
                return;
            }
            bool CanRemoveFromPockets = Pockets.Remove(iObj);
            RemainingPocketSpace += iObj.Size;
            InventoryUpdated.Invoke();

            if (!CanRemoveFromPockets)
            {
                throw new Exception("Couldn't remove item from inventory because it was not found there.");
            }
        }
        public bool TrySetActiveInventoryItem(InventoryObject iObj, bool SetActive)
        {
            if (SetActive)
            {
                if (ActiveWeapon == null && iObj is Weapon)
                {
                    ActiveWeapon = iObj as Weapon;
                    return true;
                }
                if (ActiveTool == null && !(iObj is Weapon))
                {
                    ActiveTool = iObj;
                    return true;
                }
                return false;
            }
            else
            {
                if (ActiveWeapon == iObj)
                {
                    ActiveWeapon = null;
                    return true;
                }
                if (ActiveTool == iObj)
                {
                    ActiveTool = null;
                    return true;
                }
                return false;
            }
        }
        public void SetActiveInventoryToolToNull()
        {
            if (ActiveTool != null)
            {
                TrySetActiveInventoryItem(ActiveTool, false);
            }
        }
        public void SetActiveInventoryWeaponToNull()
        {
            if (ActiveWeapon != null)
            {
                TrySetActiveInventoryItem(ActiveWeapon, false);
            }
        }
    }
}