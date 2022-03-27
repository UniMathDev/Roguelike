using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.InventoryObjects
{
    class Gun : RangedWeapon
    {
        private int ammo;
        new public int Ammo
        {
            get
            {
                return ammo;
            }
            set
            {
                ammo = value;
                string Name = $"Pistol({ammo}): ";
                string additionalDescription = ammo switch
                {
                    Magazine.maxAmmo => $"{ammo} bullets are inside it. ",
                    > 1 => $"{ammo} bullets are left inside it. ",
                    1 => $"{ammo} a single bullet is left inside it. ",
                    0 => $"{ammo} It's magazine is empty. ",
                    _ => throw new Exception("ammo count has to be positive"),
                };
                Description = Name + baseDescription + additionalDescription;
            }
        }
        string baseDescription = "A deadly black pistol. It's heavy." +
            " I think it's intuitive enough for me to use. ";
        public Gun()
        {
            AverageDamage = 110f;
            DamageRange = 0f;
            HitChance = 0.8f;
            Size = 2;
            MaxAmmo = Magazine.maxAmmo;
            Ammo = Magazine.maxAmmo;
        }
    }
}
