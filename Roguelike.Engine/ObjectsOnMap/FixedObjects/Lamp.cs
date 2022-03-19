using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    public class Lamp : FixedObject, IUsable
    {
        protected bool HasBulb;

        public Lamp() : base()
        {
            Character = 'o';
            ForegroundColor = ConsoleColor.Yellow;
            Description = "Bright power saving lamp";
            MapLayer = Enums.MapLayer.CEILING;
            Walkable = true;
        }
        public bool TryUse(object obj)
        {
            //if a lamp was given then put HasBulb to true;
            return false;
        }
    }
}
