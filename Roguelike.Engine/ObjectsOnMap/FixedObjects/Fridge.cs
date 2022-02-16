using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Fridge : FixedObject
    {
        public Fridge()
        {
            Character = 'N';
            Description = "Fridge: hope is a good breakfast, but a bad supper.";
            Seethrough = false;
        }
    }
}
