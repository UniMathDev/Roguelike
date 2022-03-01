using System.Collections.Generic;
using Roguelike.Engine.InventoryObjects;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    public class InventoryObjectOnGround : FixedObject, ISearchable
    {
        public bool WasSearched { get; private set; }
        public List<InventoryObject> Inventory { get; set; }
        public InventoryObjectOnGround() : base()
        {
            Character = '%';
            Description = "There's something on the ground here";
            Inventory = new();
        }
    }
}
