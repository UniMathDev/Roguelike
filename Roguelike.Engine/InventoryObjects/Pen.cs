namespace Roguelike.Engine.InventoryObjects
{
    public class Pen : MeleeWeapon
    {
        public Pen()
        {
            Size = 1;
            Description = "Ballpoint pen: I guess you could try using this as a weapon?..";
            AverageDamage = 10f;
            DamageRange = 5f;
            KnockBackChance = 0f;
            Durability = 4;
        }
    }
}
