using Roguelike.Engine.InventoryObjects;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Screen : FixedObject, ISearchable
    {
        public bool WasSearched { get; private set; }

        public List<InventoryObject> Inventory { get; set; } = new();

        public Screen() : base()
        {
            Character = 'D';
            Description = "Screen: For showing boring presentations. ";
            Seethrough = false;
        }
    }
}
