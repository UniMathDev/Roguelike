using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    abstract class Toilet : FixedObject
    {
        protected Toilet(char character) : base()
        {
            Character = character;
            Description = "Toilet: do you want to drink?";
        }
    }
    class FirstPartOfToilet : Toilet
    {
        public FirstPartOfToilet() : base('B')
        {
        }
    }
    class SecondPartOfToilet : Toilet
    {
        public SecondPartOfToilet() : base('O')
        {
        }
    }
}
