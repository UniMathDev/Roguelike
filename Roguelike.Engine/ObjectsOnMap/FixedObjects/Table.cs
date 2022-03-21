namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    abstract class Table : FixedObject
    {
        protected Table(char character) : base()
        {
            Character = character;
            Description = "Table: many important things are stored here. ";
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
