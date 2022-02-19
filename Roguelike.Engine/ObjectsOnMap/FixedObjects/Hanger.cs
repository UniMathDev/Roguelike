﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Hanger : FixedObject
    {
        public Hanger() : base()
        {
            Character = 'Y';
            Description = "Hanger: may be you can find something in pockets.";
        }
    }
}
