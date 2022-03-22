using Roguelike.Engine.InventoryObjects;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap
{
    public interface ILuminous
    {
        public int LightedAreaRadius { get; }
    }
}
