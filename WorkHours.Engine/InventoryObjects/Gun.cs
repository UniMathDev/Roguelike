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
            HitChance = 0.8f;
            Size = 2;
            MaxAmmo = Magazine.maxAmmo;
            Ammo = Magazine.maxAmmo;
        }
        protected override void OnAmmoChanged(int newAmmo)
        {
            string Name = $"Pistol({newAmmo}): ";
            string baseDescription = "A deadly black pistol. It's heavy." +
            " I think it's intuitive enough for me to use. ";
            string additionalDescription = newAmmo switch
            {
                Magazine.maxAmmo => $"{newAmmo} bullets are inside it. ",
                > 1 => $"{newAmmo} bullets are left inside it. ",
                1 => $"{newAmmo} a single bullet is left inside it. ",
                0 => $"{newAmmo} It's magazine is empty. ",
                _ => throw new Exception("ammo count has to be positive"),
            };
            Description = Name + baseDescription + additionalDescription;
        }
    }
}
