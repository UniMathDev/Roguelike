namespace Roguelike.Engine.InventoryObjects
{
    public abstract class Weapon : InventoryObject
    {
        public float AverageDamage { get; protected set; }
        public float DamageRange { get; protected set; }


    }
    public abstract class MeleeWeapon : Weapon
    {
        public float KnockBackChance { get; protected set; }
        public int Durability { get; set; }
    }
    public abstract class RangedWeapon : Weapon
    {
        public int MaxAmmo;
        private int ammo;
        public int Ammo { 
            get 
            {
                return ammo;
            } 
            set 
            {
                ammo = value;
                OnAmmoChanged(ammo);
            } 
        }
        public float HitChance;
        protected abstract void OnAmmoChanged(int newAmmo);
    }
}
