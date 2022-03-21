using Roguelike.Engine.InventoryObjects;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Deadbody : FixedObject, ISearchable
    {
        public bool WasSearched { get; private set; }

        public List<InventoryObject> Inventory { get; set; } = new();

        public Deadbody() : base()
        {
            Character = 'g';
            Description = "A dead person: I'm not sure if I want to get any closer. ";
            Seethrough = true;
        }
    }
}
