using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.InventoryObjects
{
    class Bandage : InventoryObject
    {
        public static float HealAmount = 20;
        public Bandage()
        {
            Size = 1;
            TwoHanded = false;
            Description = "Bandage: a sterile bandage, could be used to treat wounds. ";
        }
    }
}
