using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Screen : FixedObject
    {
        public Screen()
        {
            Character = 'D';
            Description = "Screen: information also can be a weapon.";
            Seethrough = false;
        }
    }
}
