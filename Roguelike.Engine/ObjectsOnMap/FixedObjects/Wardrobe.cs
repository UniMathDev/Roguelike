using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Wardrobe : FixedObject
    {
        public Wardrobe() : base()
        {
            Character = '█';
            Description = "Wardrobe: in addition to someone else's underwear, you can find something useful here.";
            Seethrough = false;
        }
    }
}
