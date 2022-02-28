using System.Collections.Generic;
using System;
using Roguelike.Engine.InventoryObjects;

namespace Roguelike.Engine.ObjectsOnMap
{
    public class Player : LivingObject
    {
        public PlayerInventory inventory = new PlayerInventory();
        public float Stamina { get; private set; } = 100f;
        public Player(int x, int y)
        {
            Character = '@';
            ForegroundColor = ConsoleColor.White;
            Description = "Me: thats me. ";
            Health = 100;
            X = x;
            Y = y;
        }
    }
    public class PlayerInventory
    {
        public List<InventoryObject> Pockets { get; private set; } = new List<InventoryObject>();
        private int RemainingPocketSpace = 5;
        public InventoryObject[] Hands { get; private set; } = new InventoryObject[2];
        public Weapon ActiveWeapon { get; private set; }
        public InventoryObject ActiveItem { get; private set; }

        public void AddToInventory(InventoryObject iObj)
        {
            if (!CanAddToInventory(iObj))
            {
                throw new Exception("Cant add item of this size to inventory.");
            }

            if (Hands[0] == null)
            {
                Hands[0] = iObj;
            }
            else if (Hands[1] == null)
            {
                Hands[1] = iObj;
            }
            else
            {
                Pockets.Add(iObj);
                RemainingPocketSpace -= iObj.Size;
            }
        }
        public bool CanAddToInventory(InventoryObject iObj)
        {
            if(Hands[0] == null)
            {
                return true;
            }
            if(Hands[1] == null)
            {
                return true;
            }
            if (RemainingPocketSpace - iObj.Size >= 0)
            {
                return true;
            }
            return false;
        }
        public void RemoveFromInventory(InventoryObject iObj)
        {
            if (Hands[0] == iObj)
            {
                Hands[0] = null;
                return;
            }
            if (Hands[1] == iObj)
            {
                Hands[1] = null;
                return;
            }
            bool CanRemoveFromPockets = Pockets.Remove(iObj);
            RemainingPocketSpace += iObj.Size;

            if (!CanRemoveFromPockets)
            {
                throw new Exception("Couldn't remove item from inventory because it was not found there.");
            }
        }
    }
}
