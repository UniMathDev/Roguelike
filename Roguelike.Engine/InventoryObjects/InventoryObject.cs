using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.InventoryObjects
{
    public class InventoryObject
    {
        public int Size { get; protected set; }
        public string Description { get; protected set; }
    }
}
