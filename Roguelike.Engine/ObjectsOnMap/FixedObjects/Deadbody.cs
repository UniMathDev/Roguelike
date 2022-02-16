using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Deadbody : FixedObject
    {
        public Deadbody()
        {
            Character = 'g';
            Description = "Deadbody: Are you ok to search here?";
            Seethrough = false;
        }
    }
}
