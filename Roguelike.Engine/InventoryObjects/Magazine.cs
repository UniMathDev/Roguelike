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
            Description = "Magazine (10): a full 9mm ten round magazine. ";
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
                switch (ammo)
                {
                    case 0: Description = "Magazine (0): an empty ten round magazine. "; break;
                    case 1: Description = "Magazine (1): a single bullet is left inside this one. "; break;
                    case 2: Description = "Magazine (2): 2 bullets can be seen inside this one. "; break;
                    case 3: Description = "Magazine (3): 3 bullets are left in this one. "; break;
                    case 4: Description = "Magazine (4): 4 bullets are left in this one. "; break;
                    case 5: Description = "Magazine (5): 5 bullets are left in this one. "; break;
                    case 6: Description = "Magazine (6): 6 bullets are left in this one. "; break;
                    case 7: Description = "Magazine (7): 7 bullets are left in this one. "; break;
                    case 8: Description = "Magazine (8): 8 bullets are left in this one. "; break;
                    case 9: Description = "Magazine (9): 9 bullets are left in this one. "; break;
                    case 10: Description = "Magazine (10): a full 9mm ten round magazine. "; break;
                }
            }
        }
    }
}
