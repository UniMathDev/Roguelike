namespace Roguelike.Engine.InventoryObjects
{
    public class Axe : MeleeWeapon
    {
        public Axe()
        {
            Size = 3;
            Description = "Fire axe: This looks like a formiddable weapon. ";
            AverageDamage = 110f;
            DamageRange = 20f;
            KnockBackChance = 0.7f;
            Durability = 999999;
        }
    }
}
