using Roguelike.Engine.InventoryObjects;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap
{
    public interface ISearchable
    {
        bool WasSearched { get; set; }
        List<InventoryObject> Inventory { get; }
    }
}
