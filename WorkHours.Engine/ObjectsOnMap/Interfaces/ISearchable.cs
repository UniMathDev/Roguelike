using Roguelike.Engine.InventoryObjects;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap
{
    public interface ISearchable : IChangeAble
    {
        bool WasSearched { get; }
        List<InventoryObject> Inventory { get; } 
    }
}
