using Roguelike.Engine.InventoryObjects;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Fridge : FixedObject, ISearchable
    {
        public bool WasSearched { get; private set; }

        public List<InventoryObject> Inventory { get; set; } = new();

        public Fridge() : base()
        {
            Character = 'N';
            Description = "Fridge: I'm not hungry right now. ";
            Seethrough = false;
        }
    }
}
