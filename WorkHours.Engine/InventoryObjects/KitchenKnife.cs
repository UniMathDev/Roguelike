namespace Roguelike.Engine.InventoryObjects
{
    public class KitchenKnife: MeleeWeapon
    {
        public KitchenKnife()
        {
            Size = 1;
            Description = "KitchenKnife: Everything that has a blade is a weapon. ";
            AverageDamage = 20f;
            DamageRange = 10f;
            KnockBackChance = 0.1f;
            Durability = 15;
        }
    }
}
