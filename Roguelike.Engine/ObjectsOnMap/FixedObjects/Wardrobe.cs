using Roguelike.Engine.InventoryObjects;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Wardrobe : FixedObject, ISearchable
    {
        public Wardrobe() : base()
        {
            Character = '█';
            Description = "Wardrobe: in addition to someone else's underwear, you can find something useful here.";
            Seethrough = false;
        }

        bool ISearchable.WasSearched { get; } = false;

        List<InventoryObject> ISearchable.Inventory { get; } = new();
    }
}
