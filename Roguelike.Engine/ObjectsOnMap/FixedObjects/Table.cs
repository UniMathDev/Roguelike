using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    abstract class Table : FixedObject
    {
        protected Table(char character)
        {
            Character = character;
            Description = "Table: a great pleace to keep something important.";
            Seethrough = false;
        }
    }
    class CenterOfTable : Table
    {
        //can be used to search
        public CenterOfTable() : base('═')
        {
        }
    }
    class LeftTableLeg : Table
    {
        public LeftTableLeg() : base('╒')
        {
        }
    }
    class RightTableLeg : Table
    {
        public RightTableLeg() : base('╕')
        {
        }
    }
}
