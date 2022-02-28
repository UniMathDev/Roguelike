using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.InventoryObjects
{
    public class Weapon : InventoryObject
    {
        public float AverageDamage { get; protected set; }
        public float DamageRange { get; protected set; }

    }
    public class MeleeWeapon : Weapon
    {
        public float KnockBackChance { get; protected set; }
        public int Durability { get; set; }
    }
    public class RangedWeapon : Weapon
    {
        public int MaxAmmo;
        public int Ammo;
    }
}
