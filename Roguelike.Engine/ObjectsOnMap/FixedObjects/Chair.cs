using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Chair : FixedObject
    {
        public Chair()
        {
            Character = 'h';
            Description = "Chair: there are more ways to close a door.";
            Seethrough = false;
        }
    }
}
