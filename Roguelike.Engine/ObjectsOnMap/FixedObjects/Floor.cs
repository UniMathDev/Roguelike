namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Floor : FixedObject
    {
        public Floor()
        {
            Character = '.';
            Description = "Floor: it's floor. ";
            Seethrough = true;
        }
    }
}
