namespace Roguelike.Engine.InventoryObjects
{
    public class Magazine : InventoryObject
    {
        private int ammo;
        public const int maxAmmo = 10;
        public Magazine(int ammo = maxAmmo)
        {
            Size = 1;
            Ammo = ammo;
            TwoHanded = false;
        }

        public int Ammo
        {
            get
            {
                return ammo;
            }
            set 
            { 
                ammo = value;
                string additionalDescription = "";
                switch (ammo)
                {
                    case maxAmmo: additionalDescription = $"a full 9mm {maxAmmo}-round magazine. "; break;
                    case >=2: additionalDescription = $"{ammo} bullets can be seen inside this one. "; break;
                    case 1: additionalDescription = "a single bullet is left inside this one. "; break;
                    case 0: additionalDescription = "an empty ten round magazine. "; break;
                }
                Description = $"Magazine ({ammo}): " + additionalDescription;
            }
        }
    }
}
