using Roguelike.Engine.InventoryObjects;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Hanger : FixedObject, ISearchable
    {
        public bool WasSearched { get; private set; }

        public List<InventoryObject> Inventory { get; set; } = new();

        public Hanger() : base()
        {
            Character = 'Y';
            Description = "Hanger: may be you can find something in pockets.";
            Seethrough = true;
        }
    }
}
