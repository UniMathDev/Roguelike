namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Floor : FixedObject
    {
        public Floor() : base()
        {
            Character = '.';
            Description = "Floor: it's floor. ";
            Walkable = true;
            MapLayer = Enums.MapLayer.FLOOR;
            Seethrough = true;
        }
    }
}
