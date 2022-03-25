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
            Description = "Pistol: A deadly black pistol. It's heavy. I think it's intuitive enough for me to use. ";
            AverageDamage = 110f;
            DamageRange = 0f;
            HitChance = 0.8f;
            Size = 2;
            MaxAmmo = Magazine.maxAmmo;
            Ammo = Magazine.maxAmmo;
        }
    }
}
