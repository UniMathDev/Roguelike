namespace Roguelike.Engine.InventoryObjects
{
    public class Axe : MeleeWeapon
    {
        public Axe()
        {
            Size = 6;
            Description = "Fire axe: This looks like a formiddable weapon. ";
            AverageDamage = 110f;
            DamageRange = 40f;
            KnockBackChance = 0.7f;
            Durability = 999999;
            TwoHanded = true;
        }
    }
}
