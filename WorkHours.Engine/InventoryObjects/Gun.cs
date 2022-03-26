using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.InventoryObjects
{
    class Gun : RangedWeapon
    {
        public Gun()
        {
            AverageDamage = 110f;
            DamageRange = 0f;
            Size = 2;
            MaxAmmo = 10;
            Ammo = 10;
        }
    }
}
