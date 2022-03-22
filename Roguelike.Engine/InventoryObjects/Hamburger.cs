using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.InventoryObjects
{
    public class Hamburger: InventoryObject
    {
        public Hamburger()
        {
            Size = 1;
            Description = "Hamburger: I'm not hungry right now. ";
        }
    }
}
