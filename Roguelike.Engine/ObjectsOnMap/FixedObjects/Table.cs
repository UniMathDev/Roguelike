using Roguelike.Engine.InventoryObjects;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    abstract class Table : FixedObject
    {
        protected Table(char character) : base()
        {
            Character = character;
            Description = "Table: a great pleace to keep something important.";
            Seethrough = true;
        }
    }
    class CenterOfTable : Table, ISearchable
    {
        public bool WasSearched { get; private set; }

        public List<InventoryObject> Inventory { get; set; } = new();

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
