using Roguelike.Engine.InventoryObjects;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class DeadMonster : FixedObject
    {
        public DeadMonster() : base()
        {
            MapLayer = Enums.MapLayer.SECONDARY;
            Character = 'з';
            Description = "A dead thing: it's not moving anymore. ";
            ForegroundColor = System.ConsoleColor.DarkMagenta;
            Seethrough = true;
            Walkable = true;
        }
    }
}
