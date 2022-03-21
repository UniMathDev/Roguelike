namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Deadbody : FixedObject
    {
        public Deadbody() : base()
        {
            Character = 'g';
            Description = "A dead person: I'm not sure if I want to get any closer. ";
            Seethrough = true;
        }
    }
}
